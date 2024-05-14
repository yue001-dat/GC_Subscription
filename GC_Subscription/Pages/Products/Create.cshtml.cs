using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<int>? SelectedAllergyIds { get; set; }

        [BindProperty]
        public List<int>? SelectedDietIds { get; set; }

        public CreateModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Allergies"] = new SelectList(_context.Allergy, "Id", "Name");
            ViewData["Diets"] = new SelectList(_context.Diet, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve selected allergies and diets
            var allergies = _context.Allergy.Where(a => SelectedAllergyIds.Contains(a.Id)).ToList();
            var diets = _context.Diet.Where(d => SelectedDietIds.Contains(d.Id)).ToList();

            // Assign selected allergies and diets to the product
            Product.Allergies = allergies;
            Product.Diets = diets;

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
