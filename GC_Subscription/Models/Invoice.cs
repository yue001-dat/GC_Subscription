using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace GC_Subscription.Models
{
    public class Invoice
    {
        public int Id { get; set; }
       
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }

        public string InvId { get; set; }
        public string CusId { get; set; } // Den her svarer til StripeID på Customers
        public string? SubId { get; set; } // Den har svarer til StripeID på Subscriptions

        public Invoice() { }

        public Invoice( string inv_id, 
                        long amount, 
                        string currency, 
                        string status, 
                        string cus_id, 
                        string? sub_id
            )
        {
            Amount = amount;
            Currency = currency;
            Status = status;

            InvId = inv_id;
            CusId = cus_id;
            SubId = sub_id;            
        }

    }
}
