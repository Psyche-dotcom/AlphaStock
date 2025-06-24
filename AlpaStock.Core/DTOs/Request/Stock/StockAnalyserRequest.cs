namespace AlpaStock.Core.DTOs.Request.Stock
{
    public class StockAnalyserRequest
    {
        public string symbol {  get; set; }
        public double years { get; set; }
        
        public int selection { get; set; }
        public DesiredAnnReturnReq DesiredAnnReturn { get; set; }
        public RevenueGrowthReq RevGrowth { get; set; }
        public ProfitMarginReq ProfitMargin { get; set; }
        public FreeCashFlowMarginReq FreeCashFlowMargin { get; set; }
        public PERatioReq PERatio { get; set; }
        public PFCFReq PFCF { get; set; }
    }
}
