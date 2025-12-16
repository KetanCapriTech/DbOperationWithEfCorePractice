using DbOperationWithEfCorePractice.Data;
using DbOperationWithEfCorePractice.Dtos;
using DbOperationWithEfCorePractice.Models;
using Microsoft.AspNetCore.Mvc;

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
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                IsActive = model.IsActive,
                LanguageId = model.LanguageId,
                NoOfPages = model.NoOfPages,
                Title = model.Title,
            };

             _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return Ok(model);
        }
    }
}
