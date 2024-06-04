using GC_Subscription.Data;
using GC_Subscription.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using StripeInvoice = Stripe.Invoice;

namespace GC_Subscription.Controllers
{
        public class StripeWebhook: Controller
        {

            private readonly GhostchefContext _context;

            public StripeWebhook(GhostchefContext context)
            {
                _context = context;
            }

            // /StripeWebhook/Test => Test
            [HttpGet]
            public string Test()
            {
                return "It works!";
            }

            [HttpPost]
            public async Task<IActionResult> Index()
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                try
                {
                    var stripeEvent = EventUtility.ParseEvent(json);

                    // Handle the event
                    switch (stripeEvent.Type)
                    {
                        case Events.InvoiceCreated:
                            var invoiceCreated = stripeEvent.Data.Object as StripeInvoice;
                            // Define and call a method to handle the invoice creation.
                            HandleInvoiceCreated(invoiceCreated);
                            break;

                        default:
      
                            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                            break;
                    }

                    return Ok();

                } catch (StripeException e)
                {
                    Console.WriteLine($"Stripe exception: {e.Message}");
                    return BadRequest();
                }
            }
     
            private void HandleInvoiceCreated(StripeInvoice invoice)
            {
                // For debugging
                Console.WriteLine($"Invoice was created: {invoice.Id}");

                _context.Invoice.Add(new Models.Invoice(
                        invoice.Id,
                        invoice.AmountDue,
                        invoice.Currency,
                        invoice.Status, 
                        invoice.CustomerId, 
                        invoice.SubscriptionId
                    ));

                _context.SaveChanges();
            }



    }
}
