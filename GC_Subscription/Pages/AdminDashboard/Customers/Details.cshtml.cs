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
    public class DetailsModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public DetailsModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
