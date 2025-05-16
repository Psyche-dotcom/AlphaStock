namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class MetricThird
    {
        public string ReturnOnAsset { get; set; }
        public string ReturnOnEquity { get; set; }
        public string ReturnOnInvestedCapitalTTM { get; set; }
        public string AvgROIC5yrs { get; set; }
        public string PriceToBookRatio { get; set; }

        public string CompRevGrowth3yrs { get; set; }
        public string CompRevGrowth5yrs { get; set; }
        public string CompRevGrowth10yrs { get; set; }
        public string AYearHigh { get; set; }
        public string AYearlow { get; set; }

        public string ADaylow { get; set; }
        public string ADayHigh { get; set; }

        public string priceAvg50 { get; set; }
        public string priceAvg200 { get; set; }

        public string previousClose { get; set; }
      

      
    }
}
