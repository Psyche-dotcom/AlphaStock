using AlpaStock.Core.DTOs.Response.Stock;

namespace AlpaStock.Core.DTOs.Request.Stock
{
    public class StockAnalysisHisotry
    {
        public string symbol { get; set; }
        public double years { get; set; }
        public StockAnalyserResponse stockAnalyserResponseSystemDataData { get; set; }
        public StockAnalyserRequest StockAnalyserUserDataRequest { get; set; }
        public StockAnaResponse PredictedAnalysis { get; set; }
    }
}
