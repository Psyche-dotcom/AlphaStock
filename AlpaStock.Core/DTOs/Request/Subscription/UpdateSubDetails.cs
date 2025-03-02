namespace AlpaStock.Core.DTOs.Request.Subscription
{
    public class UpdateSubDetails
    {

        public string SubPlanId { get; set; }
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public bool IsDIscounted { get; set; } = false;
        public int DiscountRate { get; set; } = 0;

        public string BillingInterval { get; set; }

    }
}
