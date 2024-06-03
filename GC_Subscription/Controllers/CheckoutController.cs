using GC_Subscription.Data;
using GC_Subscription.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GC_Subscription.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly GhostchefContext _context;

        public CheckoutController(GhostchefContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetCityByZip(int zipCode)
        {
            var city = await _context.ZipCity
                .Where(c => c.Zip == zipCode)
                .Select(c => c.City)
                .FirstOrDefaultAsync();

            if (city != null)
            {
                return Json(city);
            }
            else
            {
                return Json("Unknown");
            }
        }
    }
}
