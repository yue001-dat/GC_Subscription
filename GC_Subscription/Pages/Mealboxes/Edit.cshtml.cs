using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Mealboxes
{
    public class EditModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public EditModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mealbox Mealbox { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealbox =  await _context.Mealbox.FirstOrDefaultAsync(m => m.Id == id);
            if (mealbox == null)
            {
                return NotFound();
            }
            Mealbox = mealbox;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Mealbox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealboxExists(Mealbox.Id))
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

        private bool MealboxExists(int id)
        {
            return _context.Mealbox.Any(e => e.Id == id);
        }
    }
}
