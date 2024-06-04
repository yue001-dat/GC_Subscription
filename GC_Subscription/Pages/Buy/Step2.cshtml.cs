using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GC_Subscription.Data;
using GC_Subscription.Models;
using Stripe;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace GC_Subscription.Pages.Buy
{
    public class Step2Model : PageModel
    {
        private readonly GC_Subscription.Data.GhostchefContext _context;


        public Step2Model(GC_Subscription.Data.GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dictionary<string, string> PostData { get; private set; }

        public IActionResult OnGet()
        {

            // redirect back after dev
            return Page();
          
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            PostData = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                PostData[key] = Request.Form[key];
            }

            // Create Customer
            // Externally (Stripe)
            StripeConfiguration.ApiKey = "sk_test_V27Kw0a0YHvJO3hrT7wUHvea";

            var customerOptions = new CustomerCreateOptions
            {
                Name = Request.Form["Customer.Name"],
                Email = Request.Form["Customer.Email"],
                Phone = Request.Form["Customer.Phone"],
                PaymentMethod = Request.Form["payment_method_id"],

                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = Request.Form["payment_method_id"]
                }

            };

            var customerService = new CustomerService();
            Stripe.Customer stripeCustomerObject = customerService.Create(customerOptions);

            // Zip from String to Int
            if (int.TryParse(Request.Form["Customer.Zip"], out int zip)) { }

            var customer = new Models.Customer(
                stripeCustomerObject.Name,
                Request.Form["Customer.Address1"],
                zip,
                stripeCustomerObject.Email,
                stripeCustomerObject.Phone,
                Request.Form["Customer.Comments"],
                stripeCustomerObject.Id
            );

            // internally
            _context.Customer.Add(customer);
            _context.SaveChanges();

            // Create Subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = stripeCustomerObject.Id,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions { Price = "price_1PO5NXCY6nVmgR1jZcDNt4LD" },
                },
            };

            var subscriptionService = new SubscriptionService();
            subscriptionService.Create(subscriptionOptions);

            if (int.TryParse(Request.Form["mealbox-id"], out int mealboxId)) { }

            _context.Subscription.Add(new Models.Subscription(
                customer.Id,
                Request.Form["interval_option"],
                stripeCustomerObject.Id,
                mealboxId
                ));

            _context.SaveChanges();

            return Page();


        }
    }
}
