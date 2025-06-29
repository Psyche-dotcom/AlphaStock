using System.ComponentModel.DataAnnotations.Schema;

namespace AlpaStock.Core.Entities
{
    public class Payments : BaseEntity
    {


        public string Amount { get; set; }
        public string OrderReferenceId { get; set; }
        public string Description { get; set; }
       
        public string PaymentType { get; set; } = "PayPal";
        public DateTime CreatedPaymentTime { get; set; } = DateTime.UtcNow;
        public DateTime CompletePaymentTime { get; set; }
        public bool IsActive { get; set; } = true;
        public string PaymentStatus { get; set; } = "CREATED";

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Subscription Subscription { get; set; }
        public string SubscriptionId { get; set; }
    }
}
