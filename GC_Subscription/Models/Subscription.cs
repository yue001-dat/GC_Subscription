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
        public int Interval { get; set; }

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

        /*
        ICollection<SubscriptionProduct> SubscriptionProducts { get; set; }       
        ICollection<SubscriptionDiet> SubscriptionDiets { get; set; }
        ICollection<SubscriptionAllergy> SubscriptionAllergies { get; set; }        
        ICollection<Invoice> Invoices { get; set; }
        */
    }
}
    