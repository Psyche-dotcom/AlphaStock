namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class KeyMetricTTMRep
    {
        public string Symbol { get; set; }

        public long? MarketCap { get; set; }
        public long? EnterpriseValueTTM { get; set; }

        public double? EvToSalesTTM { get; set; }
        public double? EvToOperatingCashFlowTTM { get; set; }
        public double? EvToFreeCashFlowTTM { get; set; }
        public double? EvToEBITDATTM { get; set; }
        public double? NetDebtToEBITDATTM { get; set; }
        public double? CurrentRatioTTM { get; set; }
        public double? IncomeQualityTTM { get; set; }
        public double? GrahamNumberTTM { get; set; }
        public double? GrahamNetNetTTM { get; set; }
        public double? TaxBurdenTTM { get; set; }
        public double? InterestBurdenTTM { get; set; }

        public long? WorkingCapitalTTM { get; set; }
        public long? InvestedCapitalTTM { get; set; }

        public double? ReturnOnAssetsTTM { get; set; }
        public double? OperatingReturnOnAssetsTTM { get; set; }
        public double? ReturnOnTangibleAssetsTTM { get; set; }
        public double? ReturnOnEquityTTM { get; set; }
        public double? ReturnOnInvestedCapitalTTM { get; set; }
        public double? ReturnOnCapitalEmployedTTM { get; set; }

        public double? EarningsYieldTTM { get; set; }
        public double? FreeCashFlowYieldTTM { get; set; }

        public double? CapexToOperatingCashFlowTTM { get; set; }
        public double? CapexToDepreciationTTM { get; set; }
        public double? CapexToRevenueTTM { get; set; }

        public double? SalesGeneralAndAdministrativeToRevenueTTM { get; set; }
        public double? ResearchAndDevelopementToRevenueTTM { get; set; }
        public double? StockBasedCompensationToRevenueTTM { get; set; }
        public double? IntangiblesToTotalAssetsTTM { get; set; }

        public long? AverageReceivablesTTM { get; set; }
        public long? AveragePayablesTTM { get; set; }
        public long? AverageInventoryTTM { get; set; }

        public double? DaysOfSalesOutstandingTTM { get; set; }
        public double? DaysOfPayablesOutstandingTTM { get; set; }
        public double? DaysOfInventoryOutstandingTTM { get; set; }
        public double? OperatingCycleTTM { get; set; }
        public double? CashConversionCycleTTM { get; set; }

        public long? FreeCashFlowToEquityTTM { get; set; }
        public double? FreeCashFlowToFirmTTM { get; set; }
        public long? TangibleAssetValueTTM { get; set; }
        public long? NetCurrentAssetValueTTM { get; set; }
    }

}
