using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class StockService : IStockService
    {
        private readonly ILogger<StockService> _logger;
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly IAlphaRepository<StockWishList> _stockWishRepo;
        public StockService(ILogger<StockService> logger,
            IApiClient apiClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _apiClient = apiClient;
            _configuration = configuration;
            _baseUrl = _configuration["FMP:BASEURL"];
        }
        public async Task<ResponseDto<IEnumerable<StockResp>>> GetStockQuote(string symbol)
        {
            var response = new ResponseDto<IEnumerable<StockResp>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/quote?symbol={symbol}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock quote error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock quote error ex", makeRequest.ErrorException);
                    _logger.LogError("stock quote error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock quote" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<StockResp>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock quote is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<StockInfo>>> GetOtherStockInfo(string symbol)
        {
            var response = new ResponseDto<IEnumerable<StockInfo>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/profile?symbol={symbol}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock info error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock info error ex", makeRequest.ErrorException);
                    _logger.LogError("stock info error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock info" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<StockInfo>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock info is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<IncomeStatementResp>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequest.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<IncomeStatementResp>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Income statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock Income statement at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<BalanceSheetResp>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/balance-sheet-statement?symbol={symbol}&period={period}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequest.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<BalanceSheetResp>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock balance sheet statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"balance-sheet-statement ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock Income statement at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<CashFlowStatement>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequest.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<CashFlowStatement>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock cash flow statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"cash-flow-statement ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock cash flow statement at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }


        public async Task<ResponseDto<IEnumerable<StockDataHistoryData>>> HistoryCalPriceEOD(string symbol, string startdate, string enddate)
        {
            var response = new ResponseDto<IEnumerable<StockDataHistoryData>>();
            try
            {

   
                var apiUrl = _baseUrl + $"stable/historical-price-eod/full?symbol={symbol}&from={startdate}&to={enddate}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("historical-price-eod error mess", makeRequest.ErrorMessage);
                    _logger.LogError("historical-price-eod error ex", makeRequest.ErrorException);
                    _logger.LogError("historical-price-eod error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock historical-price-eod statement" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<StockDataHistoryData>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock historical-price-eod is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"cash-flow-statement ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock historical-price-eod at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> AddStockWishList(string userid, string stockSymbol, decimal lowerLimit, decimal upperLimit)
        {
            var response = new ResponseDto<string>();
            try
            {

                var result = await _stockWishRepo.Add(new StockWishList()
                {
                    UserId = userid,
                    StockSymbols = stockSymbol,
                    LowerLimit = lowerLimit,
                    UpperLimit = upperLimit
                });

                await _stockWishRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Stock wish list added successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Stock wish list service not available" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<IEnumerable<StockWishList>>> GetUserStockWishList(string userid)
        {
            var response = new ResponseDto<IEnumerable<StockWishList>>();
            try
            {

                var result = await _stockWishRepo.GetQueryable().Where(u=>u.UserId == userid).ToListAsync();

                ;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Get Stock wish list service not available" };
                response.StatusCode = 400;
                return response;
            }
        }

    }
}
