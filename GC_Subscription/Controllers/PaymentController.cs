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

            // Returner en succes eller fejl svar
            return Ok(new { success = true });
        }


        public class PaymentRequest
        {
            public string PaymentMethodId { get; set; }
            public int Amount { get; set; }
        }
    }
}
