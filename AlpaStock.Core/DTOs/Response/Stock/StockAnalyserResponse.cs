namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class StockAnalyserResponse
    {
        public ROIC ROIC { get; set; }
        public RevenueGrowth RevGrowth { get; set; }
        public ProfitMargin ProfitMargin { get; set; }
        public FreeCashFlowMargin FreeCashFlowMargin { get; set; }
        public PERatio PERatio { get; set; }
        public PFCF PFCF { get; set; }
    }
}
