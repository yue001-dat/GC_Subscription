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
        [Range(0, 1000, ErrorMessage = "Prisen skal være mellem 0 og 1000")]
        public int Price { get; set; }

        public List<Product> Products { get; } = [];
        public List<Subscription> Subscriptions { get; } = [];
    }
}
