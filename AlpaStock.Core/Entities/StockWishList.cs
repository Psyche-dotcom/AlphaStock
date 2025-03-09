namespace AlpaStock.Core.Entities
{
    public class StockWishList : BaseEntity
    {
        public string UserId { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public ApplicationUser User { get; set; }
        public string StockSymbols { get; set; }
    }
}
