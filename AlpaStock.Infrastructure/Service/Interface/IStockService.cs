using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Stock;
using AlpaStock.Core.DTOs.Response.Stock;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStockService
    {
        Task<ResponseDto<List<NewsItem>>> GetStockPressGeneralNews(string page, string limit);
        Task<ResponseDto<List<IncomeStatementResp>>> GetStockIncomeStatementTTM(string symbol, string period);
        Task<ResponseDto<List<BalanceSheetResp>>> GetStockBalanceSheetTTM(string symbol, string period);
        Task<ResponseDto<List<CashFlowStatement>>> GetStockCashFlowStatementTTM(string symbol, string period);
        Task<ResponseDto<List<NewsItem>>> GetGeneralStockNews(string page, string limit);
        Task<ResponseDto<List<NewsItem>>> GetStockPressNews(string symbol, string page, string limit);
        Task<ResponseDto<List<NewsItem>>> GetStockNews(string symbol, string page, string limit);
        Task<ResponseDto<string>> AddMyAlpha(string userid, List<MyAlphaReq> req);
        Task<ResponseDto<IEnumerable<StockSearchResp>>> SearchStock(string symbol);
        Task<ResponseDto<StockAnalyserResponse>> StockAnalyserRequest(string symbol, string period);
        Task<ResponseDto<FundamentalMetricData>> Metrics(string symbol, string period);
        Task<ResponseDto<List<StockDataHistoryData>>> HistoryCalPriceEOD(string symbol, string startdate, string enddate);
        Task<ResponseDto<IEnumerable<StockWishListResp>>> GetUserStockWishList(string userid);
        Task<ResponseDto<string>> AddStockWishList(string userid, string stockSymbol, decimal lowerLimit, decimal upperLimit);
        Task<ResponseDto<List<StockResp>>> GetStockQuote(string symbol);
        Task<ResponseDto<List<StockInfo>>> GetOtherStockInfo(string symbol);
        Task<ResponseDto<StockWishListResponseIsadded>> IsAddStockWishList(string userid, string stockSymbol);
        Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period, string duration);
        Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period, string duration);
        Task<ResponseDto<List<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period, string duration);
        Task<ResponseDto<StockAnaResponse>> StockAnalyserResponse(StockAnalyserRequest req);
        Task<ResponseDto<string>> UpdateStockWishList(string stockwishlistId, decimal lowerLimit, decimal upperLimit);
        Task<ResponseDto<string>> DeleteStockWishList(string stockwishlistId);
        Task<ResponseDto<StockAlphaResp>> StockAlphaRequest(string symbol, string period);
        Task<ResponseDto<List<MyAlphaReq>>> RetrieveMyAlpha(string userid);
        Task<ResponseDto<IEnumerable<Alpha8PillerResp>>> AlphaStock8Pillers(string symbol, string period);
    }
}
