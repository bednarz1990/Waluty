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
    public class DeleteModel : PageModel
    {
        private readonly Waluty.Data.WalutyContext _context;

        public DeleteModel(Waluty.Data.WalutyContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExchangeRate = await _context.ExchangeRate.FindAsync(id);

            if (ExchangeRate != null)
            {
                _context.ExchangeRate.Remove(ExchangeRate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
