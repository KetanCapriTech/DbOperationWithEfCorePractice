using DbOperationWithEfCorePractice.Data;
using DbOperationWithEfCorePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCorePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrencyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getCurrency()
        {
            var result = await _context.Currencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")] // for number time to avaoid amiquity
        public async Task<IActionResult> getCurrencyByIdAsync(int id)
        {
            var result = await _context.Currencies.FindAsync(id); // no need of where we can use direclty findAsync
            //var result = await _context.Currencies.Where(a => a.Id == id).AnyAsync(); -> this will check id is or reord is present or not if not then return false
            return Ok(result);
        }

        [HttpGet("name")]
        public async Task<IActionResult> getCurrencyByNameAsync(string name, [FromQuery] string description)
        {
            var result = await _context.Currencies.Where(s => s.CurrencyName.ToLower() == name.ToLower() && s.CurrencyDescription.ToLower() == description.ToLower()).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound("Data not found");
            }
            return Ok(result);
        }

        [HttpPost("get-by-Ids")]
        public async Task<IActionResult> getResultFromIds([FromBody] List<int> ids)
        {
            var result = await _context.Currencies.Where(s => ids.Contains(s.Id)).ToListAsync();

            return Ok(result);
        }

        [HttpPost("get-by-currencies")]
        public async Task<IActionResult> getResultFromCurrencyNames([FromBody] List<string> cname)
        {
            var result = await _context.Currencies.Where(s => cname.Contains(s.CurrencyName)).ToListAsync();

            return Ok(result);
        }

        [HttpPost("get-few-fields")]
        public async Task<IActionResult> getOnlyFewFields([FromQuery] int id)
        {
            var result = await _context.Currencies
                 .Where(s => s.Id == id)
                 .Select(x => new Currency()
                 {
                     Id = x.Id,
                     CurrencyName = x.CurrencyName
                 }).FirstOrDefaultAsync();

            return Ok(result);
        }

        [HttpPost("get-few-fileds-multi-ids")]
        public async Task<IActionResult> getFewFieldsFromMultipleIds([FromBody] List<int> ids)
        {
            var result = await _context.Currencies.Where(s => ids.Contains(s.Id))
                .Select(x => new Currency()
                {
                    Id = x.Id,
                    CurrencyDescription = x.CurrencyDescription
                }).ToListAsync();

            return Ok(result);
        }
    }
}
