using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GC_Subscription.Models
{
    public class Product
    {
        // Product Fields
        public int Id { get; set; }

        [Required(ErrorMessage ="Angiv venligst et navn til retten")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Angiv venligst en beskrivelse af retten")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Angiv venligst et beløb")]
        [Range(1, 1000, ErrorMessage = "Prisen skal være mellem 1 og 1000")]
        public int Price { get; set; }

        public bool InStock { get; set; } = true;
        
        // TODO: Adding this back, crashes the app on Product creation. Why? Sunrays or Lucifer... I dunno
        public string? ImageUrl { get; set; } 

        // Relational Fields
        public ICollection<Mealbox> Mealboxes { get; } = [];
        public ICollection<Allergy> Allergies { get; set; } = [];
        public ICollection<Diet> Diets { get; set; } = [];
        
    }
}
