using DbOperationWithEfCorePractice.Data;
using DbOperationWithEfCorePractice.Dtos;
using DbOperationWithEfCorePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCorePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController(ApplicationDbContext _context) : ControllerBase
    {
       [HttpPost("add-library")]
       public IActionResult AddLibrary([FromBody] LibraryDto model)
       {
            Library library = new Library()
            {
                BooksSection = model.BooksSection.Trim(),
                BookType = model.BookType.Trim(),
                NumberOfBooks = model.NumberOfBooks,
                NumberOfBooksPerSection = model.NumberOfBooksPerSection,
                SectionInfo = model.SectionInfo.Trim(),
                LibrarianId = model.LibrarianId,
                Librarian = new Librarian
                {
                    FirstName = model.Librarian!.FirstName.Trim(),
                    LastName = model.Librarian!.LastName.Trim(),
                    Address = model.Librarian.Address.Trim(),
                    ContactNo = model.Librarian.ContactNo.Trim(),
                    Email = model.Librarian.Email.Trim(),
                    CreatedOn = DateTime.UtcNow,
                    HighestQualification = model.Librarian.HighestQualification.Trim(),
                    IsFullTime = model.Librarian.IsFullTime,
                    IsActive = model.Librarian.IsActive
                }
            };

            _context.library.Add(library);

            _context.SaveChanges();

            return Ok(library);
       }

       [HttpGet("get-list-of-library")]
       public async Task<IActionResult> GetListOfLibrary()
        {
            var result = await _context.library.Where(s => s.Librarian.IsActive == true).ToListAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

       [HttpGet("get-library-by-id")]
       public async Task<IActionResult> GetLibraryById([FromQuery] int id)
        {
            var result = await _context.library.Include(s => s.Librarian).Where(s => s.Id == id && s.Librarian.IsActive == true).FirstOrDefaultAsync();

            if( result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

       [HttpPut("update-library-info")]
       public async Task<IActionResult> UpdateLibraryInfo([FromBody] LibraryDto model)
       {
            var existingLab = await _context.library
                .Include(x => x.Librarian)
                .Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if(existingLab == null)
            {
                return NotFound();
            }

            existingLab.SectionInfo = model.SectionInfo;
            existingLab.BooksSection = model.BooksSection;
            existingLab.NumberOfBooksPerSection = model.NumberOfBooksPerSection;
            existingLab.NumberOfBooks = model.NumberOfBooks;
            existingLab.BookType = model.BookType;
            existingLab.Librarian.UpdatedOn = DateTime.UtcNow;
            existingLab.Librarian.ContactNo = model.Librarian.ContactNo;
            existingLab.Librarian.Address = model.Librarian.Address;
            existingLab.Librarian.Email = model.Librarian.Email;
            existingLab.Librarian.FirstName = model.Librarian.FirstName;
            existingLab.Librarian.LastName = model.Librarian.LastName;
            existingLab.Librarian.HighestQualification = model.Librarian.HighestQualification;
            existingLab.Librarian.IsFullTime = model.Librarian.IsFullTime;
            existingLab.Librarian.IsActive = model.Librarian.IsActive;

            await _context.SaveChangesAsync();

            return Ok(existingLab);
           
       }

       [HttpDelete("delete-library-by-id")]
       public async Task<IActionResult> DeleteLibraryById([FromQuery] int id)
       {
            var getLab = await _context.library
                .Include(s => s.Librarian)
                .Where(s => s.Id == id && s.IsActive == true).FirstOrDefaultAsync();

            if (getLab == null)
            {
                return NotFound();
            }

            getLab.IsActive = false;
            getLab.Librarian.IsActive = false;

            _context.Update(getLab);

            await _context.SaveChangesAsync();

            return Ok(getLab);
       }

       [HttpPost("toggle-job")]
       public async Task<IActionResult> ToggleJob([FromQuery] int id)
        {
            var getUser = await _context.library
                .Include(s => s.Librarian)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if(getUser == null)
            {
                return NotFound();
            }

           //if( getUser.Librarian.IsFullTime == true)
           // {
           //     getUser.Librarian.IsFullTime = false;
           // }
           // else
           // {
           //     getUser.Librarian.IsFullTime = true;
           // }

            // simple way to do filp the value
            getUser.Librarian.IsFullTime = !getUser.Librarian.IsFullTime ;

            await _context.SaveChangesAsync();
           return Ok(getUser);
        }
    }
}
