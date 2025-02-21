namespace AlpaStock.Core.Entities
{
    public class Subscription : BaseEntity
    {
        public string SubType { get; set; }
        public string Module { get; set; }
        public double Amount { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
    }
}
