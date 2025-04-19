namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class StockDataHistoryData
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
        public decimal Vwap { get; set; }
    }
}
