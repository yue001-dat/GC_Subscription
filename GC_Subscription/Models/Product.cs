using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GC_Subscription.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // PK

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Ingredients { get; set; }

        public bool InStock { get; set; } = true;

    }
}
