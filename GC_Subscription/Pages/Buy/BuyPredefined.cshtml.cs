
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Buy
{
    public class BuyPredefined : PageModel
    {
        private readonly GhostchefContext _context;
        public List<Product> AvailableProducts { get; set; } = default!;
        public List<Allergy>? AvailableAllergies { get; set; }
        public List<Diet>? AvailableDiets { get; set; }

        public BuyPredefined(GhostchefContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; } = default!;

        public Mealbox Mealbox { get; set; } = default!;
        public List<string> UniqueDiets { get; set; } = new List<string>();
        public List<string> UniqueAllergies { get; set; } = new List<string>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if(id == null)
            {
                id = 1;
            }

            var mealbox = await _context.Mealbox
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Diets)
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Allergies)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (mealbox == null)
            {
                return NotFound();
            }

            Mealbox = mealbox;

            UniqueDiets = mealbox.Products
                                 .SelectMany(p => p.Diets)
                                 .Select(d => d.Name)
                                 .Distinct()
                                 .ToList();

            UniqueAllergies = mealbox.Products
                                     .SelectMany(p => p.Allergies)
                                     .Select(a => a.Name)
                                     .Distinct()
                                     .ToList();

            //Subscription = await _context.Subscription
             //   .Include(s => s.Customer).ToListAsync();

            //AvailableProducts = await _context.Product.ToListAsync(); // Det her er alle som er til rådighed
            //AvailableAllergies = await _context.Allergy.ToListAsync();  // Det her er alle som er til rådighed
            //AvailableDiets = await _context.Diet.ToListAsync();  // Det her er alle som er til rådighed

            AvailableProducts = mealbox.Products.ToList<Product>(); // Det her er dem som er på den specifikke måltidskasse
            AvailableAllergies = mealbox.Products.SelectMany(p => p.Allergies).ToList(); // Det her er dem som er på den specifikke måltidskasse
            AvailableDiets = mealbox.Products.SelectMany(p => p.Diets).ToList(); // Det her er dem som er på den specifikke måltidskasse

            return Page();
        }
       

    }
}
