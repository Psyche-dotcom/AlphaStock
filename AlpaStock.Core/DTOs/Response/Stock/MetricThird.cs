namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class MetricThird
    {
      
        public string ReturnOnAsset { get; set; }
        public string ReturnOnEquity { get; set; }
        public string ReturnOnInvestedCapitalTTM { get; set; }
        public string ReturnOnInvestedCapital5year { get; set; }
        public string AYearHigh { get; set; }
        public string AYearlow { get; set; }
  
        public string ADaylow { get; set; }
        public string ADayHigh { get; set; }
      
        public string priceAvg50 { get; set; }
        public string priceAvg200 { get; set; }
  
        public string previousClose { get; set; }
    }
}
