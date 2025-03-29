using System.IO.Pipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.data;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public CurrencyController(AppDBContext appDBContext) {
            _appDBContext = appDBContext;
        }

        //this api return all data
        [HttpGet("/allcurrencies")]
        public async Task<IActionResult> GetAllCurrencies() {
            //var result = this.appDBContext.Currency.ToList();

            //var result = (from currencies in _appDBContext.Currency
            //             select currencies).ToList();

            //var result = await _appDBContext.Currency.ToListAsync();
            var result = await (from currencies in _appDBContext.Currency
                                select currencies).ToListAsync();
            return Ok(result);
        }


        [HttpGet("/currency/{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await _appDBContext.Currency.FindAsync(id);

            if(result == null)
            {
                return NotFound(new { message = $"Currency with ID {id} not found." });
            }
            return Ok(result);
        }

        [HttpGet("/currency/{name}")]
        public async Task<IActionResult> GetCurrenyByName([FromRoute] string name)
        {
            //var result = await _appDBContext.Currency.Where(x => x.Title == name).FirstOrDefaultAsync();
            var result = await _appDBContext.Currency.FirstOrDefaultAsync(x => x.Title == name);
            return Ok(result);
        }

        //https://localhost:7004/currencies/INR?descrip=Indian INR
        [HttpGet("/currencies/{name}")]
        public async  Task<IActionResult> GetCurrencyByNameAndQuery([FromRoute] string name, [FromQuery] string? descrip)
        {
             //getting for single record
            var result = await _appDBContext.Currency.FirstOrDefaultAsync(
                x=>x.Title == name && (string.IsNullOrEmpty(descrip) || x.Description == descrip)
                );

            //getting for multiple records
            var resultall = await _appDBContext.Currency.Where(x => x.Title == name && (string.IsNullOrEmpty(descrip) || x.Description == descrip)).ToListAsync();
            return Ok(resultall);
        }

        [HttpPost("/all")]
        public async Task<IActionResult> GetCurrencyall([FromBody] List<int> ids)
        {

            //return all records that present in list and
            //var ids = new List<int> { 1, 3 };
            //this return all columns
            //var resultall = await _appDBContext.Currency.Where(x => ids.Contains(x.Id)).ToListAsync();

            //this return only that we want columns
            var resultall = await _appDBContext.Currency.Where(x => ids.Contains(x.Id)).Select(x=> new Currency()
            {
                Id = x.Id,
                Title=x.Title,
            }).ToListAsync();


            //or but this return all data
            var result = (from currencies in _appDBContext.Currency
                         select new Currency()
                         {
                             Id= currencies.Id,
                             Title =currencies.Title,
                         }).ToList();

            //AnonymousObject, but this return all data
            var result1 = (from currencies in _appDBContext.Currency
                          select new 
                          {
                              Id = currencies.Id,
                              Title = currencies.Title,
                          }).ToList();

            return Ok(result1);
        }




    }
}
