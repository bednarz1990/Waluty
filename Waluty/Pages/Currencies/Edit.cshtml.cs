using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Waluty.Data;
using Waluty.Models;

namespace Waluty
{
    public class EditModel : PageModel
    {
        private readonly Waluty.Data.WalutyContext _context;

        public EditModel(Waluty.Data.WalutyContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ExchangeRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRateExists(ExchangeRate.ExchangeRateId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExchangeRateExists(int id)
        {
            return _context.ExchangeRate.Any(e => e.ExchangeRateId == id);
        }
    }
}
