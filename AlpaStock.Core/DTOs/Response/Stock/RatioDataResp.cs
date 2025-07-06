namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class RatioDataResp
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public string ReportedCurrency { get; set; }

        public double? GrossProfitMargin { get; set; }
        public double? EbitMargin { get; set; }
        public double? EbitdaMargin { get; set; }
        public double? OperatingProfitMargin { get; set; }
        public double? PretaxProfitMargin { get; set; }
        public double? ContinuousOperationsProfitMargin { get; set; }
        public double? NetProfitMargin { get; set; }
        public double? BottomLineProfitMargin { get; set; }
        public double? ReceivablesTurnover { get; set; }
        public double? PayablesTurnover { get; set; }
        public double? InventoryTurnover { get; set; }
        public double? FixedAssetTurnover { get; set; }
        public double? AssetTurnover { get; set; }
        public double? CurrentRatio { get; set; }
        public double? QuickRatio { get; set; }
        public double? SolvencyRatio { get; set; }
        public double? CashRatio { get; set; }
        public double? PriceToEarningsRatio { get; set; }
        public double? PriceToEarningsGrowthRatio { get; set; }
        public double? ForwardPriceToEarningsGrowthRatio { get; set; }
        public double? PriceToBookRatio { get; set; }
        public double? PriceToSalesRatio { get; set; }
        public double? PriceToFreeCashFlowRatio { get; set; }
        public double? PriceToOperatingCashFlowRatio { get; set; }
        public double? DebtToAssetsRatio { get; set; }
        public double? DebtToEquityRatio { get; set; }
        public double? DebtToCapitalRatio { get; set; }
        public double? LongTermDebtToCapitalRatio { get; set; }
        public double? FinancialLeverageRatio { get; set; }
        public double? WorkingCapitalTurnoverRatio { get; set; }
        public double? OperatingCashFlowRatio { get; set; }
        public double? OperatingCashFlowSalesRatio { get; set; }
        public double? FreeCashFlowOperatingCashFlowRatio { get; set; }
        public double? DebtServiceCoverageRatio { get; set; }
        public double? InterestCoverageRatio { get; set; }
        public double? ShortTermOperatingCashFlowCoverageRatio { get; set; }
        public double? OperatingCashFlowCoverageRatio { get; set; }
        public double? CapitalExpenditureCoverageRatio { get; set; }
        public double? DividendPaidAndCapexCoverageRatio { get; set; }
        public double? DividendPayoutRatio { get; set; }
        public double? DividendYield { get; set; }
        public double? DividendYieldPercentage { get; set; }
        public double? RevenuePerShare { get; set; }
        public double? NetIncomePerShare { get; set; }
        public double? InterestDebtPerShare { get; set; }
        public double? CashPerShare { get; set; }
        public double? BookValuePerShare { get; set; }
        public double? TangibleBookValuePerShare { get; set; }
        public double? ShareholdersEquityPerShare { get; set; }
        public double? OperatingCashFlowPerShare { get; set; }
        public double? CapexPerShare { get; set; }
        public double? FreeCashFlowPerShare { get; set; }
        public double? NetIncomePerEBT { get; set; }
        public double? EbtPerEbit { get; set; }
        public double? PriceToFairValue { get; set; }
        public double? DebtToMarketCap { get; set; }
        public double? EffectiveTaxRate { get; set; }
        public double? EnterpriseValueMultiple { get; set; }
        public double? DividendPerShare { get; set; }
    }

}
