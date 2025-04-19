namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class CashFlowStatement
    {
        public DateTime? Date { get; set; }
        public string Symbol { get; set; }
        public string ReportedCurrency { get; set; }
        public string Cik { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public long? NetIncome { get; set; }
        public long? DepreciationAndAmortization { get; set; }
        public long? DeferredIncomeTax { get; set; }
        public long? StockBasedCompensation { get; set; }
        public long? ChangeInWorkingCapital { get; set; }
        public long? AccountsReceivables { get; set; }
        public long? Inventory { get; set; }
        public long? AccountsPayables { get; set; }
        public long? OtherWorkingCapital { get; set; }
        public long? OtherNonCashItems { get; set; }
        public long? NetCashProvidedByOperatingActivities { get; set; }
        public long? InvestmentsInPropertyPlantAndEquipment { get; set; }
        public long? AcquisitionsNet { get; set; }
        public long? PurchasesOfInvestments { get; set; }
        public long? SalesMaturitiesOfInvestments { get; set; }
        public long? OtherInvestingActivities { get; set; }
        public long? NetCashProvidedByInvestingActivities { get; set; }
        public long? NetDebtIssuance { get; set; }
        public long? LongTermNetDebtIssuance { get; set; }
        public long? ShortTermNetDebtIssuance { get; set; }
        public long? NetStockIssuance { get; set; }
        public long? NetCommonStockIssuance { get; set; }
        public long? CommonStockIssuance { get; set; }
        public long? CommonStockRepurchased { get; set; }
        public long? NetPreferredStockIssuance { get; set; }
        public long? NetDividendsPaid { get; set; }
        public long? CommonDividendsPaid { get; set; }
        public long? PreferredDividendsPaid { get; set; }
        public long? OtherFinancingActivities { get; set; }
        public long? NetCashProvidedByFinancingActivities { get; set; }
        public long? EffectOfForexChangesOnCash { get; set; }
        public long? NetChangeInCash { get; set; }
        public long? CashAtEndOfPeriod { get; set; }
        public long? CashAtBeginningOfPeriod { get; set; }
        public long? OperatingCashFlow { get; set; }
        public long? CapitalExpenditure { get; set; }
        public double? FreeCashFlow { get; set; }
        public long? IncomeTaxesPaid { get; set; }
        public long? InterestPaid { get; set; }
    }
}
