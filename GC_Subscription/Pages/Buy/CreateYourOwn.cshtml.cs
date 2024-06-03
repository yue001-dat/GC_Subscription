
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Buy
{
    public class CreateYourOwn : PageModel
    {
        private readonly GhostchefContext _context;
        public List<Product> AvailableProducts { get; set; } = default!;
        public List<Allergy>? AvailableAllergies { get; set; }
        public List<Diet>? AvailableDiets { get; set; }

        public CreateYourOwn(GhostchefContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; } = default!;

        public Mealbox Mealbox { get; set; } = default!;
        public List<string> UniqueDiets { get; set; } = new List<string>();
        public List<string> UniqueAllergies { get; set; } = new List<string>();


        public async Task<IActionResult> OnGetAsync()
        {
            Subscription = await _context.Subscription
                .Include(s => s.Customer).ToListAsync();

            AvailableProducts = await _context.Product.ToListAsync(); // Det her er alle som er til rådighed
            AvailableAllergies = await _context.Allergy.ToListAsync();  // Det her er alle som er til rådighed
            AvailableDiets = await _context.Diet.ToListAsync();  // Det her er alle som er til rådighed


            return Page();
        }
       

    }
}
