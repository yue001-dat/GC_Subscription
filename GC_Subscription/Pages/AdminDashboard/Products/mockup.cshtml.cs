using GC_Subscription.Data;
using GC_Subscription.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GC_Subscription.Pages.AdminDashboard.Products
{
    public class MockupModel : PageModel
    {
        private readonly GhostchefContext _context;
        public IList<Product> Product { get; set; } = default!;
        public MockupModel(GhostchefContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Product = await _context.Product
                                    .Include(p => p.Allergies)
                                    .Include(p => p.Diets)
                                    .ToListAsync();
        }
    }
}
