using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Waluty.Data;
using Waluty.Models;

namespace Waluty
{
    public class CreateModel : PageModel
    {
        private readonly Waluty.Data.WalutyContext _context;

        public CreateModel(Waluty.Data.WalutyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExchangeRate ExchangeRate { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ExchangeRate.Rates = new List<Rate> { new Rate { EffectiveDate = DateTime.Now } };
            _context.ExchangeRate.Add(ExchangeRate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
