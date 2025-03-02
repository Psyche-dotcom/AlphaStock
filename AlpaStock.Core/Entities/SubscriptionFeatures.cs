using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class SubscriptionFeatures : BaseEntity
    {
        public string Category { get; set; }
        public string FeatureName { get; set; }
        public string ShortName { get; set; }
        public string SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public string CurrentState { get; set; }
    }
}
