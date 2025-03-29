using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.data;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(AppDBContext _appdbcontext) : ControllerBase
    {
        //private readonly AppDBContext _appDbContext;

        //public BookController(AppDBContext appDbContext) {
        //    _appDbContext = appDbContext;
        //}

        //one record add at time

        [HttpPost("/addbook")]
        public async Task<IActionResult> addBook([FromBody] Book modal)
        {
            _appdbcontext.Book.Add(modal);

            await _appdbcontext.SaveChangesAsync();
            return Ok(modal);
        }

        [HttpPost("/addmultplebook")]
        public async Task<IActionResult> addMulBook([FromBody] List<Book> modal)
        {
            _appdbcontext.Book.AddRange(modal);

            await _appdbcontext.SaveChangesAsync();
            return Ok(modal);
        }

        [HttpGet("/getAllBooks")]
        public async Task<IActionResult> getAllBooks()
        {
            var result = await _appdbcontext.Book.ToListAsync();
            return Ok(result);
        }
    }
}
