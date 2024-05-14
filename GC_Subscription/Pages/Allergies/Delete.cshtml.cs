using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Allergies
{
    public class DeleteModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public DeleteModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Allergy Allergy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await _context.Allergy.FirstOrDefaultAsync(m => m.Id == id);

            if (allergy == null)
            {
                return NotFound();
            }
            else
            {
                Allergy = allergy;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await _context.Allergy.FindAsync(id);
            if (allergy != null)
            {
                Allergy = allergy;
                _context.Allergy.Remove(Allergy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
