namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class MetricFourth
    {
        public string AYearHigh { get; set; }
        public string AYearlow { get; set; }

        public string ADaylow { get; set; }
        public string ADayHigh { get; set; }

        public string priceAvg50 { get; set; }
        public string priceAvg200 { get; set; }

        public string previousClose { get; set; }
    }
}
