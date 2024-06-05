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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GC_Subscription.Pages.Buy
{
    public class Step1Model : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public Dictionary<string, string> PostData { get; private set; }

        [BindProperty]
        public List<int> SelectedDietIds { get; set; } = new List<int>();
        public IList<Diet> DietList { get; set; } = new List<Diet>();
        
        [BindProperty]
        public List<int> SelectedAllergyIds { get; set; } = new List<int>();
        public IList<Allergy> AllergyList { get; set; } = new List<Allergy>();


        private readonly StripeSettings _stripeSettings;

        public string PublicKey => _stripeSettings.PublicKey;

        public Step1Model(GC_Subscription.Data.GhostchefContext context, StripeSettings stripeSettings)
        {
            _context        = context;
            _stripeSettings = stripeSettings;
        }

        public IActionResult OnGet()
        {
            return Page();

            //return RedirectToPage("Index"); // Uncomment this and remove line above in production
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        public Mealbox Mealbox { get; set; } = default!;
  
        public int MealBoxId { get; set; }

        // Product Selections
        public Product ProductDay1 { get; set; } = default!; 
        public Product ProductDay2 { get; set; } = default!; 
        public Product ProductDay3 { get; set; } = default!; 
        public Product ProductDay4 { get; set; } = default!; 

      
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Used to display post data on site while developing
            // remove in production
            PostData = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                PostData[key] = Request.Form[key];
            }

          
            // Fetch Mealbox When selling predefined
            if (int.TryParse(Request.Form["mealbox-id"], out int mealBoxId))
            {
                MealBoxId = mealBoxId;
            }

            var mealbox = await _context.Mealbox
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Diets)
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Allergies)
                                        .FirstOrDefaultAsync(m => m.Id == MealBoxId);

            if (mealbox == null)
            {
                return NotFound();
            }

            // MealBox
            Mealbox = mealbox;

            // Product Selection
            var productDay1 = await GetProductFromFormInputAsync("product_day_1");
            ProductDay1 = productDay1;
            
            var productDay2 = await GetProductFromFormInputAsync("product_day_2");
            ProductDay2 = productDay2;

            var productDay3 = await GetProductFromFormInputAsync("product_day_3");
            ProductDay3 = productDay3;
            
            var productDay4 = await GetProductFromFormInputAsync("product_day_4");
            ProductDay4 = productDay4;

            if (SelectedDietIds != null && SelectedDietIds.Count > 0)
            {
                DietList = await _context.Diet
                                         .Where(d => SelectedDietIds.Contains(d.Id))
                                         .ToListAsync();
            }

            if (SelectedAllergyIds != null && SelectedAllergyIds.Count > 0)
            {
                AllergyList = await _context.Allergy
                                         .Where(d => SelectedAllergyIds.Contains(d.Id))
                                         .ToListAsync();
            }

            Subscription subscription = new Subscription();

            return Page();
        }


        public async Task<Product> GetProductFromFormInputAsync(string formInputKey)
        {
            // Hent produkt-ID'et fra formularens input-felt ved hjælp af formInputKey
            var productId = Request.Form[formInputKey];

            // Konverter produkt-ID'et til en integer, hvis nødvendigt
            if (int.TryParse(productId, out int productDayId))
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productDayId);         
                return product;
            }

            return null;
        }


    }
        
}
