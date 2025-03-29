using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Stock;
using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStockService
    {
        Task<ResponseDto<StockAnalyserResponse>> StockAnalyserRequest(string symbol, string period);
        Task<ResponseDto<FundamentalMetricData>> Metrics(string symbol, string period);
        Task<ResponseDto<IEnumerable<StockDataHistoryData>>> HistoryCalPriceEOD(string symbol, string startdate, string enddate);
        Task<ResponseDto<IEnumerable<StockWishListResp>>> GetUserStockWishList(string userid);
        Task<ResponseDto<string>> AddStockWishList(string userid, string stockSymbol, decimal lowerLimit, decimal upperLimit);
        Task<ResponseDto<List<StockResp>>> GetStockQuote(string symbol);
        Task<ResponseDto<List<StockInfo>>> GetOtherStockInfo(string symbol);
        Task<ResponseDto<bool>> IsAddStockWishList(string userid, string stockSymbol);
        Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period);
        Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period);
        Task<ResponseDto<IEnumerable<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period);
        Task<ResponseDto<StockAnaResponse>> StockAnalyserResponse(StockAnalyserRequest req);
        Task<ResponseDto<string>> UpdateStockWishList(string stockwishlistId, decimal lowerLimit, decimal upperLimit);
        Task<ResponseDto<string>> DeleteStockWishList(string stockwishlistId);
    }
}
