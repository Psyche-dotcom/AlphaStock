namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class MetricFirst
    {
        public double RevenuePerShare { get; set; }
        public long Revenue { get; set; }
        public long NetIcomeTTM { get; set; }
        public string FiveyearAvgNetIncome { get; set; }
        public string PToERatioFive5yrs { get; set; }
        public string PToERatio { get; set; }
        public string PSRatio { get; set; }
        public string PSRatioFive5yrs { get; set; }
        public string ProfitMarginTTM { get; set; }
        public string GrossProfitMargin { get; set; }
    }
}
