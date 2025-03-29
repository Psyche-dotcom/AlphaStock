namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class UpdateStockWishlistReq
    {
        public string StockwishlistId { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
    }
}
