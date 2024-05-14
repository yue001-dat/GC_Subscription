using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Models;

namespace GC_Subscription.Data
{
    public class GhostchefContext : DbContext
    {
        public GhostchefContext (DbContextOptions<GhostchefContext> options)
            : base(options)
        {
        }

        public DbSet<GC_Subscription.Models.Product> Product { get; set; } = default!;
        public DbSet<GC_Subscription.Models.Mealbox> Mealbox { get; set; } = default!;
        public DbSet<GC_Subscription.Models.Diet> Diet { get; set; } = default!;
        public DbSet<GC_Subscription.Models.Allergy> Allergy { get; set; } = default!;
    }
}
