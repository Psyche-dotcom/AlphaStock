namespace AlpaStock.Core.DTOs.Request.Stock
{
    public class StockWishListResp
    {
        public string Id { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public double Price { get; set; }
        public string StockSymbols { get; set; }
        public string? ImgUrl { get; set; }
    }
}
