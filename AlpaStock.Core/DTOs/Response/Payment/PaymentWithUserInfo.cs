using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Payment
{
    public class PaymentWithUserInfo
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string OrderReferenceId { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreatedPaymentTime { get; set; }
        public DateTime CompletePaymentTime { get; set; }
        public bool IsActive { get; set; }
        public string PaymentStatus { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string SubscriptionTypeName { get; set; }
        public string Email { get; set; }
    }

}
