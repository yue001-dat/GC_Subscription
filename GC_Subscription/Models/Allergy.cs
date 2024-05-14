using System.ComponentModel.DataAnnotations;

namespace GC_Subscription.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Angiv venligst et navn på allergien")]
        public string Name { get; set; }

        public List<Product> Products { get; } = [];
    }
}
