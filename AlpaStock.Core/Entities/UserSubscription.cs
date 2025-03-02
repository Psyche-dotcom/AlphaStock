using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class UserSubscription : BaseEntity
    {
        public Subscription Subscription { get; set; }
        public string SubscriptionId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime SubscrptionStart { get; set; }
        public DateTime SubscrptionEnd { get; set; }
    }
}
