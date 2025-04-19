using System;

namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class BalanceSheetResp
    {
        public string Date { get; set; }
        public string Symbol { get; set; }
        public string ReportedCurrency { get; set; }
        public string Cik { get; set; }
        public string FilingDate { get; set; }
        public string AcceptedDate { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public long? CashAndCashEquivalents { get; set; }
        public long? ShortTermInvestments { get; set; }
        public long? CashAndShortTermInvestments { get; set; }
        public long? NetReceivables { get; set; }
        public long? AccountsReceivables { get; set; }
        public long? OtherReceivables { get; set; }
        public long? Inventory { get; set; }
        public long? Prepaids { get; set; }
        public long? OtherCurrentAssets { get; set; }
        public long? TotalCurrentAssets { get; set; }
        public long? PropertyPlantEquipmentNet { get; set; }
        public long? Goodwill { get; set; }
        public long? IntangibleAssets { get; set; }
        public long? GoodwillAndIntangibleAssets { get; set; }
        public long? LongTermInvestments { get; set; }
        public long? TaxAssets { get; set; }
        public long? OtherNonCurrentAssets { get; set; }
        public long? TotalNonCurrentAssets { get; set; }
        public long? OtherAssets { get; set; }
        public long? TotalAssets { get; set; }
        public long? TotalPayables { get; set; }
        public long? AccountPayables { get; set; }
        public long? OtherPayables { get; set; }
        public long? AccruedExpenses { get; set; }
        public long? ShortTermDebt { get; set; }
        public long? CapitalLeaseObligationsCurrent { get; set; }
        public long? TaxPayables { get; set; }
        public long? DeferredRevenue { get; set; }
        public long? OtherCurrentLiabilities { get; set; }
        public long? TotalCurrentLiabilities { get; set; }
        public long? LongTermDebt { get; set; }
        public long? DeferredRevenueNonCurrent { get; set; }
        public long? DeferredTaxLiabilitiesNonCurrent { get; set; }
        public long? OtherNonCurrentLiabilities { get; set; }
        public long? TotalNonCurrentLiabilities { get; set; }
        public long? OtherLiabilities { get; set; }
        public long? CapitalLeaseObligations { get; set; }
        public long? TotalLiabilities { get; set; }
        public long? TreasuryStock { get; set; }
        public long? PreferredStock { get; set; }
        public long? CommonStock { get; set; }
        public long? RetainedEarnings { get; set; }
        public long? AdditionalPaidInCapital { get; set; }
        public long? AccumulatedOtherComprehensiveIncomeLoss { get; set; }
        public long? OtherTotalStockholdersEquity { get; set; }
        public long? TotalStockholdersEquity { get; set; }
        public double? TotalEquity { get; set; }
        public long? MinorityInterest { get; set; }
        public long? TotalLiabilitiesAndTotalEquity { get; set; }
        public long? TotalInvestments { get; set; }
        public double? TotalDebt { get; set; }
        public long? NetDebt { get; set; }
    }
}
