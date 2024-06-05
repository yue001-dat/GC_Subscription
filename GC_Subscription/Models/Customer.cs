using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GC_Subscription.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id               { get; set; } // PK

        [Required]
        [Display(Name = "Dit fulde navn")]
        [StringLength(64)]
        public string Name          { get; set; }
          
        [Required]
        [Display(Name = "Adresse")]
        public string Address1      { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        public int Zip { get; set; }

        //public ZipCity City { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Din E-mail")]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefon/Mobil")]
        public string? Phone { get; set; }

        [Display(Name = "Bemærkninger")]
        public string? Comments { get; set; } = "Så lad gå da, et 02!";

        // This if for later
        public DateTime CreatedAt { get; set; }
        public bool SignupCompleted { get; set; } = false;
        public string StripeId { get; set; }

        public Customer() {}
        
        public Customer(string name, string address1, int zip, string email, string phone, string comment, string stripe_id)
        {
            Name = name;
            Address1 = address1;
            Zip = zip;  
            Email = email;
            Phone = phone;
            Comments    = comment;
            StripeId = stripe_id;

            CreatedAt = DateTime.Now;
            SignupCompleted = false;
        }




    
    }
}
