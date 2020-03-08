using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waluty.Data;
using Waluty.Models;

namespace Waluty
{
    public class DetailsModel : PageModel
    {
        private readonly Waluty.Data.WalutyContext _context;

        public DetailsModel(Waluty.Data.WalutyContext context)
        {
            _context = context;
        }

        public ExchangeRate ExchangeRate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExchangeRate = await _context.ExchangeRate.FirstOrDefaultAsync(m => m.ExchangeRateId == id);

            if (ExchangeRate == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
