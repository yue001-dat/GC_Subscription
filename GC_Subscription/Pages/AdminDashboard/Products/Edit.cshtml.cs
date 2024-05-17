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

namespace GC_Subscription.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<Allergy> AvailableAllergies { get; set; }
        public List<Diet> AvailableDiets { get; set; }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public List<int>? SelectedAllergyIds { get; set; }

        [BindProperty]
        public List<int>? SelectedDietIds { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }


        public EditModel(GhostchefContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Include(p => p.Allergies)
                                                .Include(p => p.Diets)
                                                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            AvailableAllergies = await _context.Allergy.ToListAsync();
            AvailableDiets = await _context.Diet.ToListAsync();

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

            // Get current product from DB
            var existingProduct = await _context.Product.FirstOrDefaultAsync(p => p.Id == Product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = Product.Name;
                existingProduct.Description = Product.Description;
                existingProduct.Price = Product.Price;

                // Image proces
                if (Image != null && Image.Length > 0)
                {
                    var folderName = "images";
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var filePath = Path.Combine(uploadDir, uniqueFileName);

                    // Save image to server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // Update ImageUrl property
                    existingProduct.ImageUrl = $"/{folderName}/" + uniqueFileName;
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }


        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
