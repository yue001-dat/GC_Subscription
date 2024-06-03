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

namespace GC_Subscription.Pages.Buy
{
    public class Step1Model : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public Dictionary<string, string> PostData { get; private set; }

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

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

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

            Mealbox = mealbox;

            return Page();

            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            */
        }
    }
}
