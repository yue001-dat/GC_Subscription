using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.AdminDashboard.Customers
{
    public class IndexModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public IndexModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customer
                .Include(c => c.City).ToListAsync();
        }
    }
}
