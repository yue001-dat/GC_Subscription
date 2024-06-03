using System.ComponentModel.DataAnnotations;

namespace GC_Subscription.Models
{
    public class Mealbox
    {
        // Product Fields
        public int Id { get; set; }

        [Required(ErrorMessage = "Angiv venligst et navn til måltidskassen")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Angiv venligst en beskrivelse af måltidskassen")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Angiv venligst et beløb")]
        [Range(1, int.MaxValue, ErrorMessage = "Angiv venligst et beløb")]
        public int Price { get; set; }

        public string? ImageUrl { get; set; }
        public string? Theme { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime LastEdited {  get; set; }

        // Relational Fields
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Subscription> Subscriptions { get; set; } = [];

        /**
         * Calculate Pricing for people > 1 using the "Price" as BasePrice and add some discounts
         **/
        public int getPrice(int peopleOptions)
        {
            if (peopleOptions <= 1)
            {
                return Price;
            }

            int BasePrice = Price;
            double totalPrice = 0;
            double discountRate = 0.05;

            for (int i = 1; i <= peopleOptions; i++)
            {
                double currentPrice = BasePrice * (1 - discountRate);
                totalPrice += currentPrice;
                discountRate = Math.Max(0, discountRate - 0.02);
            }

            return (int)Math.Round(totalPrice);
        }
    }
}
