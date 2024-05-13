using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Subscriptions
{
    public class IndexModel : PageModel
    {
        private readonly GC_Subscription.Data.GCContext _context;

        public IndexModel(GC_Subscription.Data.GCContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Subscription = await _context.Subscription
                .Include(s => s.Customer).ToListAsync();
        }
    }
}
