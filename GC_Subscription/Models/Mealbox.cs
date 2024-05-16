using System.ComponentModel.DataAnnotations;

namespace GC_Subscription.Models
{
    public class Mealbox
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Angiv venligst et navn til måltidskassen")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Angiv venligst en beskrivelse af måltidskassen")]
        public string Description { get; set; }

        [Range(0, 10000, ErrorMessage = "Prisen skal være mellem 0 og 10000")]
        public int Price { get; set; }

        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Subscription> Subscriptions { get; set; } = [];
    }
}
