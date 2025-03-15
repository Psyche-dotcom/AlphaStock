using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStockService
    {
        Task<ResponseDto<IEnumerable<StockDataHistoryData>>> HistoryCalPriceEOD(string symbol, string startdate, string enddate);
        Task<ResponseDto<IEnumerable<StockWishList>>> GetUserStockWishList(string userid);
        Task<ResponseDto<string>> AddStockWishList(string userid, string stockSymbol, decimal lowerLimit, decimal upperLimit);
        Task<ResponseDto<IEnumerable<StockResp>>> GetStockQuote(string symbol);
        Task<ResponseDto<IEnumerable<StockInfo>>> GetOtherStockInfo(string symbol);
        Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period);
        Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period);
        Task<ResponseDto<IEnumerable<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period);
    }
}
