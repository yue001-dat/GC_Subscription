using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace GC_Subscription.Controllers
{
    public class PaymentController : Controller
    {

        [HttpPost]
        public IActionResult ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Invalid payment request.");
            }

            // Logik her hvor man snkker med Stripe og bla bla 

            
            // Returner en succes eller fejl svar
            return Ok(new { success = true });
        }


        public class PaymentRequest
        {
            public string PaymentMethodId { get; set; }
            public int Amount { get; set; }
        }

        /*

        public string Index()
        {
            return "This is my default action...";
        }


        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
        */
    }
}
