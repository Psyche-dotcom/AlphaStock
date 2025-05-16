namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class FinancialGrowth
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string FiscalYear { get; set; }
        public string Period { get; set; }
        public string ReportedCurrency { get; set; }

        public double? GrowthRevenue { get; set; }
        public double? GrowthCostOfRevenue { get; set; }
        public double? GrowthGrossProfit { get; set; }
        public double? GrowthGrossProfitRatio { get; set; }
        public double? GrowthResearchAndDevelopmentExpenses { get; set; }
        public double? GrowthGeneralAndAdministrativeExpenses { get; set; }
        public double? GrowthSellingAndMarketingExpenses { get; set; }
        public double? GrowthOtherExpenses { get; set; }
        public double? GrowthOperatingExpenses { get; set; }
        public double? GrowthCostAndExpenses { get; set; }
        public double? GrowthInterestIncome { get; set; }
        public double? GrowthInterestExpense { get; set; }
        public double? GrowthDepreciationAndAmortization { get; set; }
        public double? GrowthEBITDA { get; set; }
        public double? GrowthOperatingIncome { get; set; }
        public double? GrowthIncomeBeforeTax { get; set; }
        public double? GrowthIncomeTaxExpense { get; set; }
        public double? GrowthNetIncome { get; set; }
        public double? GrowthEPS { get; set; }
        public double? GrowthEPSDiluted { get; set; }
        public double? GrowthWeightedAverageShsOut { get; set; }
        public double? GrowthWeightedAverageShsOutDil { get; set; }
        public double? GrowthEBIT { get; set; }
        public double? GrowthNonOperatingIncomeExcludingInterest { get; set; }
        public double? GrowthNetInterestIncome { get; set; }
        public double? GrowthTotalOtherIncomeExpensesNet { get; set; }
        public double? GrowthNetIncomeFromContinuingOperations { get; set; }
        public double? GrowthOtherAdjustmentsToNetIncome { get; set; }
        public double? GrowthNetIncomeDeductions { get; set; }
    }

}
