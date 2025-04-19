namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class IncomeStatementResp
    {
        public DateTime? Date { get; set; }
        public string Symbol { get; set; }
        public string ReportedCurrency { get; set; }
        public string Cik { get; set; }
        public string FilingDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public double? Revenue { get; set; }
        public long? CostOfRevenue { get; set; }
        public long? GrossProfit { get; set; }
        public long? ResearchAndDevelopmentExpenses { get; set; }
        public long? GeneralAndAdministrativeExpenses { get; set; }
        public long? SellingAndMarketingExpenses { get; set; }
        public long? SellingGeneralAndAdministrativeExpenses { get; set; }
        public long? OtherExpenses { get; set; }
        public long? OperatingExpenses { get; set; }
        public long? CostAndExpenses { get; set; }
        public long? NetInterestIncome { get; set; }
        public long? InterestIncome { get; set; }
        public long? InterestExpense { get; set; }
        public long? DepreciationAndAmortization { get; set; }
        public long? Ebitda { get; set; }
        public long? Ebit { get; set; }
        public long? NonOperatingIncomeExcludingInterest { get; set; }
        public long? OperatingIncome { get; set; }
        public long? TotalOtherIncomeExpensesNet { get; set; }
        public long? IncomeBeforeTax { get; set; }
        public long? IncomeTaxExpense { get; set; }
        public long? NetIncomeFromContinuingOperations { get; set; }
        public long? NetIncomeFromDiscontinuedOperations { get; set; }
        public long? OtherAdjustmentsToNetIncome { get; set; }
        public double? NetIncome { get; set; }
        public long? NetIncomeDeductions { get; set; }
        public long? BottomLineNetIncome { get; set; }
        public double? Eps { get; set; }
        public double? EpsDiluted { get; set; }
        public double? WeightedAverageShsOut { get; set; }
        public long? WeightedAverageShsOutDil { get; set; }
    }
}
