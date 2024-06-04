using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GC_Subscription.Models
{
    public class Subscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // PK

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string Interval { get; set; }

        public enum DefaultIntervalType {
            [Display(Name = "Every X Day")] Day,
            [Display(Name = "Every X Week")] Week,            
            [Display(Name = "Every X Month")] Month,
            [Display(Name = "Every X Year")] Year
        }

        public enum AlternativeIntervalType
        {
            [Display(Name = "Uge til uge")] 
            weekly,

            [Display(Name = "Hver anden uge")] 
            bi_weekly,

            [Display(Name = "Hver tredje uge")] 
            tri_weekly,

            [Display(Name = "Månedligt")] 
            monthly,
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public string StripeId { get; set; }


        public Subscription() { }

        public Subscription(int customer_id, string interval_option, string stripe_id, int? MealboxId)
        {
            CustomerId = customer_id;
            Interval = interval_option;
            StripeId = stripe_id;
            StartDate = DateTime.Now;
        }

        /*
        ICollection<SubscriptionProduct> SubscriptionProducts { get; set; }       
        ICollection<SubscriptionDiet> SubscriptionDiets { get; set; }
        ICollection<SubscriptionAllergy> SubscriptionAllergies { get; set; }             
        public ICollection<Invoice> Invoices { get; set; }
        */
        
    }
}
    