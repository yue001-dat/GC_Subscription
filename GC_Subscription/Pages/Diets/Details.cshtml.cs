using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Diets
{
    public class DetailsModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public DetailsModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public Diet Diet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diet.FirstOrDefaultAsync(m => m.Id == id);
            if (diet == null)
            {
                return NotFound();
            }
            else
            {
                Diet = diet;
            }
            return Page();
        }
    }
}
