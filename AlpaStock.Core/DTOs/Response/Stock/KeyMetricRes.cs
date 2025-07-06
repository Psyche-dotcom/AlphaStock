namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class KeyMetricRes
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public string ReportedCurrency { get; set; }

        public long? MarketCap { get; set; }
        public long? EnterpriseValue { get; set; }

        public double? EvToSales { get; set; }
        public double? EvToOperatingCashFlow { get; set; }
        public double? EvToFreeCashFlow { get; set; }
        public double? EvToEBITDA { get; set; }
        public double? NetDebtToEBITDA { get; set; }
        public double? CurrentRatio { get; set; }
        public double? IncomeQuality { get; set; }
        public double? TaxBurden { get; set; }
        public double? InterestBurden { get; set; }

        public long? WorkingCapital { get; set; }
        public long? InvestedCapital { get; set; }

        public string ReturnOnAssets { get; set; }
        public double? OperatingReturnOnAssets { get; set; }
        public double? ReturnOnTangibleAssets { get; set; }
        public double? ReturnOnEquity { get; set; }
        public double? ReturnOnInvestedCapital { get; set; }
        public double? ReturnOnCapitalEmployed { get; set; }
        public double? EarningsYield { get; set; }
        public double? FreeCashFlowYield { get; set; }

        public double? CapexToOperatingCashFlow { get; set; }
        public double? CapexToDepreciation { get; set; }
        public double? CapexToRevenue { get; set; }

        public double? SalesGeneralAndAdministrativeToRevenue { get; set; }
        public double? ResearchAndDevelopementToRevenue { get; set; }
        public double? StockBasedCompensationToRevenue { get; set; }
        public double? IntangiblesToTotalAssets { get; set; }

        public long? AverageReceivables { get; set; }
        public long? AveragePayables { get; set; }
        public long? AverageInventory { get; set; }

        public double? DaysOfSalesOutstanding { get; set; }
        public double? DaysOfPayablesOutstanding { get; set; }
        public double? DaysOfInventoryOutstanding { get; set; }
        public double? OperatingCycle { get; set; }
        public double? CashConversionCycle { get; set; }

        public long? FreeCashFlowToEquity { get; set; }
        public double? FreeCashFlowToFirm { get; set; }
        public long? TangibleAssetValue { get; set; }
        public long? NetCurrentAssetValue { get; set; }
    }

}
