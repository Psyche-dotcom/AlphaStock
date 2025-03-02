namespace AlpaStock.Core.Entities
{
    public class Subscription : BaseEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsDIscounted { get; set; } = false;
        public int DiscountRate { get; set; } = 0;
        public string BillingInterval { get; set; }
        public IEnumerable<SubscriptionFeatures> SubscriptionFeatures { get; set; }
        public ICollection<UserSubscription> SubscriptionsUser { get; set; }
        public ICollection<Payments> Payments { get; set; }
    }
}
