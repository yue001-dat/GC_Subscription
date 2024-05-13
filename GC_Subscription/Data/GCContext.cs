using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Models;

namespace GC_Subscription.Data
{
    public class GCContext : DbContext
    {
        public GCContext (DbContextOptions<GCContext> options)
            : base(options)
        {
        }

        public DbSet<GC_Subscription.Models.Subscription> Subscription { get; set; } = default!;
        public DbSet<GC_Subscription.Models.Customer> Customer { get; set; } = default!;
    }
}
