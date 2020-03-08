using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waluty.Data;
using Waluty.Models;

namespace Waluty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly WalutyContext _context;

        public CurrenciesController(WalutyContext context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetExchangeRate()
        {
            return await _context.ExchangeRate.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{code}")]
        public ActionResult<RatesPaged> GetExchangeRateWithFilter([FromQuery] ExchangeFilterModel filter, string code)
        {
            //Filtering logic  
            IEnumerable<Rate> FilterData(ExchangeFilterModel filterModel)
            {
                var exchangeRates = _context.ExchangeRate.Where(p => p.Code == code);
                return exchangeRates.SelectMany(p => p.Rates)
                    .ToList()
                    .Where(p => p.EffectiveDate >= filterModel.MinDate && p.EffectiveDate <= filterModel.MaxDate)
                    .Skip((filterModel.Page - 1) * filter.Limit)
                    .Take(filterModel.Limit);
            }

            var result = new PagedCollectionResponse<Rate> {Items = FilterData(filter)};
            decimal avg = default;
            if (result.Items.Any())
            {
                avg = result.Items.Select(p => p.MidPrice).Average();
            }

            return new ActionResult<RatesPaged>(new RatesPaged() { AveragePrice = avg, RatePagedCollectionResponse = result });
        }

        // PUT: api/Currencies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExchangeRate(int id, ExchangeRate ExchangeRate)
        {
            if (id != ExchangeRate.ExchangeRateId)
            {
                return BadRequest();
            }

            _context.Entry(ExchangeRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Currencies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExchangeRate>> PostExchangeRate(ExchangeRate ExchangeRate)
        {
            _context.ExchangeRate.Add(ExchangeRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRate", new { id = ExchangeRate.ExchangeRateId }, ExchangeRate);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExchangeRate>> DeleteExchangeRate(int id)
        {
            var ExchangeRate = await _context.ExchangeRate.FindAsync(id);
            if (ExchangeRate == null)
            {
                return NotFound();
            }

            _context.ExchangeRate.Remove(ExchangeRate);
            await _context.SaveChangesAsync();

            return ExchangeRate;
        }

        private bool ExchangeRateExists(int id)
        {
            return _context.ExchangeRate.Any(e => e.ExchangeRateId == id);
        }
    }
}
