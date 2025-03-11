namespace AlpaStock.Core.DTOs.Request.Stock
{
    public class AddToWishListReq
    {
        public string StockSymbol { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
    }
}
