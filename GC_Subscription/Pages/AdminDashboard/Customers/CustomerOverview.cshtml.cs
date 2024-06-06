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
    public class CustomerOverviewModel : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;

        public CustomerOverviewModel(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; } = default!;

        public IList<Subscription> Subscription { get; set; } = default!;
        public IList<Invoice> Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // fetch customer
            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }

            // fetch subscriptions
            var subscription = await _context.Subscription.Where(m => m.CustomerId == Customer.Id).Include(s => s.Customer).ToListAsync();
            Subscription = subscription;

            // fetch invoices
            var invoices = await _context.Invoice.Where(m => m.CusId == Customer.StripeId).ToListAsync();
            Invoice = invoices;

            return Page();
        }
    }
}
