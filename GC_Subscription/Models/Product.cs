using System.ComponentModel.DataAnnotations;

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
        [Range(0, 1000, ErrorMessage ="Prisen skal være mellem 0 og 10000")]
        public int Price { get; set; }
        public bool InStock { get; set; } = true;

        // Relational Fields
        public List<Mealbox> Mealboxes { get; } = [];
        public List<Allergy> Allergies { get; set; } = [];
        public List<Diet> Diets { get; set; } = [];
        
    }
}
