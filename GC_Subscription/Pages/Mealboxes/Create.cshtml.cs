using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GC_Subscription.Data;
using GC_Subscription.Models;
using Microsoft.EntityFrameworkCore;

namespace GC_Subscription.Pages.Mealboxes
{
    public class CreateModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        [BindProperty]
        public Mealbox Mealbox { get; set; }

        public List<Product> Products { get; set; }

        [BindProperty]
        public List<int> SelectedProductIds { get; set; }

        public CreateModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _context.Product.ToListAsync();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Mealbox.Products = await _context.Product.Where(p => SelectedProductIds.Contains(p.Id)).ToListAsync();

            _context.Mealbox.Add(Mealbox);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
