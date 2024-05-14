using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Mealboxes
{
    public class CreateModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public CreateModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Mealbox Mealbox { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mealbox.Add(Mealbox);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
