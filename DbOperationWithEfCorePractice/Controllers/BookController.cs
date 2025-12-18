using DbOperationWithEfCorePractice.Data;
using DbOperationWithEfCorePractice.Dtos;
using DbOperationWithEfCorePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DbOperationWithEfCorePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] BookDto model)
        {
            Book book = new Book()
            {
                AuthorName = model.AuthorName,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                IsActive = model.IsActive,
                LanguageId = model.LanguageId,
                NoOfPages = model.NoOfPages,
                Title = model.Title,
                Author = new Author
                { 
                   AuthorId = model.Author.AuthorId, 
                  AuthorName =  model.Author.AuthorName
                }
                
            };

             _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet("get-book-by-id")]
        public async Task<IActionResult> GetBookById([FromQuery] int id)
        {
            var result = await _context.Books.Where(s => s.Id == id && s.IsActive == true).FirstOrDefaultAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // get related properties concept using annon function
        [HttpGet("get-book-only-few-fileds")]
        public async Task<IActionResult> GetBookByOnlyFewFileds([FromQuery] int id)
        {
            var result = await _context.Books.Select(s => new
            {
                s.Id,
                s.Title,
                s.Description,  
                s.IsActive,
                
            }).FirstOrDefaultAsync();

            return Ok($"{result} {id}");

        }

        [HttpGet("get-book-eager")]
        public async Task<IActionResult> GetLanguagesEagerLoadingExample()
        {
            var book = await _context.Books.FirstOrDefaultAsync();

           //await _context.Entry(book).Reference(s => s.Language).LoadAsync();
            await _context.Entry(book).Reference(s => s.Author).LoadAsync();

            return Ok(book);
        }

        [HttpGet("get-book-implicit")]
        public async Task<IActionResult> GetListImplicitLoading()
        {
            var langauanges = await _context.Languages.ToListAsync();

            foreach(var lanaguage in langauanges)
            {
                _context.Entry(lanaguage).Collection(x=>x.Books).LoadAsync();
            }

            return Ok(langauanges);

        }

        // get data using sql query using fromSqlRaw
        [HttpGet("get-book-sql")]
        public async Task<IActionResult> GetBookSqlExample()
        {
            var book = await _context.Books.FromSqlRaw(@"SELECT * FROM public.""Books""").FirstOrDefaultAsync();

            return Ok(book);
        }

        [HttpPost("add-list-of-books")]
        public async Task<IActionResult> AddBooks([FromBody] List<BookDto> model) {

            List<Book> books = model.Select(dto => new Book
            {
                AuthorName = dto.AuthorName,
                CreatedOn = DateTime.UtcNow,
                Description = dto.Description,
                IsActive = dto.IsActive,
                LanguageId = dto.LanguageId,
                NoOfPages = dto.NoOfPages,
                Title = dto.Title
            }).ToList();

            _context.Books.AddRange(books);

            await _context.SaveChangesAsync();

            return Ok(model);

        }

        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBook([FromQuery] int id, BookDto model)
        {
            // eager loading (all data we will bring first)
            // i want to get books inlcuding author table so using include for that and then i want to access author table properties so that why using thenInlcude for email
            // one to one reletation 
            var result = await _context.Books
                .Include(s => s.Author)
                //.Include(l => l.Language)
                //.ThenInclude(s => s.LanguageName)
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            if(result == null)
            {
                return NotFound();
            }

            result.Title = model.Title;
            result.Description = model.Description;
            result.NoOfPages = model.NoOfPages;
            result.LanguageId = model.LanguageId;
            result.Author.AuthorName = model.Author.AuthorName;

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("delete-book-by-id")]
        public async Task<IActionResult> DeleteBookSoftDelete([FromQuery] int id)
        {
            var result = await _context.Books.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (result == null) { 
                return NotFound();
            }

            result.IsActive = false;

            await _context.SaveChangesAsync();
            return Ok("deleted");
        }

    }
}
