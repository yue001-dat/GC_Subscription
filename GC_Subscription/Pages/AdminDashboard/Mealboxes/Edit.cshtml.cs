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
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public List<Product> AvailableProducts { get; set; }

        [BindProperty]
        public Mealbox Mealbox { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedProductIds { get; set; }

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

            var mealbox =  await _context.Mealbox.Include(m => m.Products)
                                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (mealbox == null)
            {
                return NotFound();
            }
            Mealbox = mealbox;

            AvailableProducts = await _context.Product.ToListAsync();
            
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

            // Get current mealbox from DB
            var existingMealbox = await _context.Mealbox.FirstOrDefaultAsync(m => m.Id == Mealbox.Id);

            if (existingMealbox != null)
            {
                existingMealbox.Name = Mealbox.Name;
                existingMealbox.Description = Mealbox.Description;
                existingMealbox.Price = Mealbox.Price;
                existingMealbox.LastEdited = DateTime.Now;

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
                    existingMealbox.ImageUrl = $"/{folderName}/" + uniqueFileName;
                }
            }

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
