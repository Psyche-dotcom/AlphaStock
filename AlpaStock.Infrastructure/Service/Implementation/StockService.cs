using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Stock;
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
        private readonly IAlphaRepository<UserSavePiller> _userSavePillerRepo;
        public StockService(ILogger<StockService> logger,
            IApiClient apiClient,
            IConfiguration configuration,
            IAlphaRepository<StockWishList> stockWishRepo,
            IAlphaRepository<UserSavePiller> userSavePillerRepo)
        {
            _logger = logger;
            _apiClient = apiClient;
            _configuration = configuration;
            _baseUrl = _configuration["FMP:BASEURL"];
            _stockWishRepo = stockWishRepo;
            _userSavePillerRepo = userSavePillerRepo;
        }
        public async Task<ResponseDto<List<StockResp>>> GetStockQuote(string symbol)
        {
            var response = new ResponseDto<List<StockResp>>();
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
                var result = JsonConvert.DeserializeObject<List<StockResp>>(makeRequest.Content);
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
        public async Task<ResponseDto<List<NewsItem>>> GetStockNews(string symbol, string page, string limit)
        {
            var response = new ResponseDto<List<NewsItem>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/news/stock?symbols={symbol}&page={page}&limit={limit}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock news error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock news error ex", makeRequest.ErrorException);
                    _logger.LogError("stock news error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock news" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<List<NewsItem>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock news is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock news list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock news list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<NewsItem>>> GetStockPressNews(string symbol, string page, string limit)
        {
            var response = new ResponseDto<List<NewsItem>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/news/press-releases?symbols={symbol}&page={page}&limit={limit}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock news press-releases error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock news press-releases error ex", makeRequest.ErrorException);
                    _logger.LogError("stock news press-releases error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock press-releases news" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<List<NewsItem>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock press-releases news is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock news list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock press-releases news list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<NewsItem>>> GetStockPressGeneralNews(string page, string limit)
        {
            var response = new ResponseDto<List<NewsItem>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/news/press-releases-latest?page={page}&limit={limit}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock news press-releases general error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock news press-releases general error ex", makeRequest.ErrorException);
                    _logger.LogError("stock news press-releases general error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock press-releases news" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<List<NewsItem>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock press-releases general news is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock news list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the general stock press-releases news list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<NewsItem>>> GetGeneralStockNews(string page, string limit)
        {
            var response = new ResponseDto<List<NewsItem>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/news/stock-latest?page={page}&limit={limit}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("stock news stock-latest error mess", makeRequest.ErrorMessage);
                    _logger.LogError("stock news stock-latest error ex", makeRequest.ErrorException);
                    _logger.LogError("stock news stock-latest error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock stock-latest news" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<List<NewsItem>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock stock-latest news is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock stock-latest list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock stock-latest news list at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<StockInfo>>> GetOtherStockInfo(string symbol)
        {
            var response = new ResponseDto<List<StockInfo>>();
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
                var result = JsonConvert.DeserializeObject<List<StockInfo>>(makeRequest.Content);
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
        public async Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period, string duration)
        {
            var response = new ResponseDto<IEnumerable<IncomeStatementResp>>();
            try
            {

                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}&limit={duration}";
                var makeRequestIncome = await _apiClient.GetAsync<string>(apiUrlIcome);
                if (!makeRequestIncome.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome = JsonConvert.DeserializeObject<IEnumerable<IncomeStatementResp>>(makeRequestIncome.Content);
                if (!resultIncome.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Income statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultIncome;
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
        public async Task<ResponseDto<List<IncomeStatementResp>>> GetStockIncomeStatementTTM(string symbol, string period)
        {
            var response = new ResponseDto<List<IncomeStatementResp>>();
            try
            {

                var apiUrlIcome2 = _baseUrl + $"stable/income-statement-ttm?symbol={symbol}&period={period}&limit=1";
                var makeRequestIncome2 = await _apiClient.GetAsync<string>(apiUrlIcome2);
                if (!makeRequestIncome2.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome2.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome2.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome2.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome2 = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome2.Content);



                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultIncome2;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock list ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock Income statement ttm at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period, string duration)
        {
            var response = new ResponseDto<IEnumerable<BalanceSheetResp>>();
            try
            {

                var apiUrlBalance = _baseUrl + $"stable/balance-sheet-statement?symbol={symbol}&period={period}&limit={duration}";
                var makeRequestBalance = await _apiClient.GetAsync<string>(apiUrlBalance);
                if (!makeRequestBalance.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequestBalance.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequestBalance.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequestBalance.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var resultBalance = JsonConvert.DeserializeObject<IEnumerable<BalanceSheetResp>>(makeRequestBalance.Content);
                if (!resultBalance.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock balance sheet statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultBalance;
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
        public async Task<ResponseDto<List<BalanceSheetResp>>> GetStockBalanceSheetTTM(string symbol, string period)
        {
            var response = new ResponseDto<List<BalanceSheetResp>>();
            try
            {

                var apiUrlBalance = _baseUrl + $"stable/balance-sheet-statement-ttm?symbol={symbol}&period={period}&limit=1";
                var makeRequestBalance = await _apiClient.GetAsync<string>(apiUrlBalance);
                if (!makeRequestBalance.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequestBalance.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequestBalance.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequestBalance.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var resultBalance = JsonConvert.DeserializeObject<List<BalanceSheetResp>>(makeRequestBalance.Content);
                if (!resultBalance.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock balance sheet statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultBalance;
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
        public async Task<ResponseDto<List<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period, string duration)
        {
            var response = new ResponseDto<List<CashFlowStatement>>();
            try
            {

                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}&limit={duration}";
                var makeRequestCash = await _apiClient.GetAsync<string>(apiUrlCash);
                if (!makeRequestCash.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }
                var resultCash = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCash.Content);
                if (!resultCash.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock cash flow statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultCash;
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
        public async Task<ResponseDto<List<CashFlowStatement>>> GetStockCashFlowStatementTTM(string symbol, string period)
        {
            var response = new ResponseDto<List<CashFlowStatement>>();
            try
            {

                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement-ttm?symbol={symbol}&period={period}&limit=1";
                var makeRequestCash = await _apiClient.GetAsync<string>(apiUrlCash);
                if (!makeRequestCash.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }
                var resultCash = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCash.Content);
                if (!resultCash.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock cash flow statement is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultCash;
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


        public async Task<ResponseDto<List<StockDataHistoryData>>> HistoryCalPriceEOD(string symbol, string startdate, string enddate)
        {
            var response = new ResponseDto<List<StockDataHistoryData>>();
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
                var result = JsonConvert.DeserializeObject<List<StockDataHistoryData>>(makeRequest.Content);
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
                _logger.LogError($"historical-price-eod ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock historical-price-eod at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }


        public async Task<ResponseDto<IEnumerable<StockSearchResp>>> SearchStock(string symbol)
        {
            var response = new ResponseDto<IEnumerable<StockSearchResp>>();
            try
            {

                var apiUrl = _baseUrl + $"stable/search-name?query={symbol}&limit=20";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrl);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("search stock error mess", makeRequest.ErrorMessage);
                    _logger.LogError("search stock error ex", makeRequest.ErrorException);
                    _logger.LogError("search stock error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the search stock" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<StockSearchResp>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "search stock is empty" };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"search stock ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the search stock at the moment" };
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
                var check = await _stockWishRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid &&
                u.StockSymbols.ToLower() == stockSymbol.ToLower());
                if (check != null)
                {
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock already added to wishlist" };
                    response.StatusCode = 400;
                    return response;
                }
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

        public async Task<ResponseDto<StockWishListResponseIsadded>> IsAddStockWishList(string userid, string stockSymbol)
        {
            var response = new ResponseDto<StockWishListResponseIsadded>();
            try
            {
                var check = await _stockWishRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid &&
                u.StockSymbols.ToLower() == stockSymbol.ToLower());
                if (check != null)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = new StockWishListResponseIsadded()
                    {
                        IsAdded = true,
                        WishListId = check.Id
                    };
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = new StockWishListResponseIsadded()
                {
                    IsAdded = false,
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "unable to check Stock wish list service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> UpdateStockWishList(string stockwishlistId, decimal lowerLimit, decimal upperLimit)
        {
            var response = new ResponseDto<string>();
            try
            {
                var retrieve = await _stockWishRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == stockwishlistId);
                if (retrieve == null)
                {

                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock wishlist is available" };
                    return response;
                }
                retrieve.LowerLimit = lowerLimit;
                retrieve.UpperLimit = upperLimit;
                _stockWishRepo.Update(retrieve);

                await _stockWishRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Stock wishlist updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Update Stock wishlist service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> DeleteStockWishList(string stockwishlistId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var retrieve = await _stockWishRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == stockwishlistId);
                if (retrieve == null)
                {

                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock wishlist is available" };
                    return response;
                }

                _stockWishRepo.Delete(retrieve);

                await _stockWishRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Stock wishlist deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Delete stock wishlist service not available" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<IEnumerable<StockWishListResp>>> GetUserStockWishList(string userid)
        {
            var response = new ResponseDto<IEnumerable<StockWishListResp>>();
            try
            {

                var result = await _stockWishRepo.GetQueryable().Where(u => u.UserId == userid).ToListAsync();
                var Data = new List<StockWishListResp>();
                foreach (var item in result)
                {
                    var getQuote = await GetOtherStockInfo(item.StockSymbols);
                    if (getQuote.StatusCode == 200)
                    {
                        Data.Add(new StockWishListResp()
                        {
                            LowerLimit = item.LowerLimit,
                            UpperLimit = item.UpperLimit,
                            StockSymbols = item.StockSymbols,
                            Id = item.Id,
                            Price = getQuote.Result[0].Price,
                            ImgUrl = getQuote.Result[0]?.Image,

                        });
                    }
                }

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = Data;
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



        public async Task<ResponseDto<FundamentalMetricData>> Metrics(string symbol, string period)
        {
            var response = new ResponseDto<FundamentalMetricData>();
            var metricFirst = new MetricFirst();
            var metricSecond = new MetricSecond();
            var metricThird = new MetricThird();
            var metricFourth = new MetricFourth();
            try
            {
                var getQuote = await GetStockQuote(symbol);
                if (getQuote.StatusCode != 200)
                {
                    response.StatusCode = getQuote.StatusCode;
                    response.DisplayMessage = getQuote.DisplayMessage;
                    response.ErrorMessages = getQuote.ErrorMessages;
                    return response;
                }

                var apiUrlKey = _baseUrl + $"stable/key-metrics?symbol={symbol}&limit=5&period={period}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrlKey);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("key-metrics error mess", makeRequest.ErrorMessage);
                    _logger.LogError("key-metrics error ex", makeRequest.ErrorException);
                    _logger.LogError("key-metrics error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock key-metrics" };
                    return response;
                }
                var result = JsonConvert.DeserializeObject<List<KeyMetricRes>>(makeRequest.Content);
                if (!result.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock key-metrics is empty" };
                    return response;
                }

                var apiUrlRatio = _baseUrl + $"stable/ratios?symbol={symbol}&limit=5&period={period}";
                var makeRequestRatio = await _apiClient.GetAsync<string>(apiUrlRatio);
                if (!makeRequestRatio.IsSuccessful)
                {
                    _logger.LogError("Ratio error mess", makeRequestRatio.ErrorMessage);
                    _logger.LogError("Ratio error ex", makeRequestRatio.ErrorException);
                    _logger.LogError("Ratio error con", makeRequestRatio.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Ratio" };
                    return response;
                }
                var resultRatio = JsonConvert.DeserializeObject<List<RatioDataResp>>(makeRequestRatio.Content);
                if (!resultRatio.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Ratio is empty" };
                    return response;
                }

                var apiUrlKeyTTM = _baseUrl + $"stable/key-metrics-ttm?symbol={symbol}&limit=5";
                var makeRequestTTM = await _apiClient.GetAsync<string>(apiUrlKeyTTM);
                if (!makeRequestTTM.IsSuccessful)
                {
                    _logger.LogError("key-metricsTTM error mess", makeRequestTTM.ErrorMessage);
                    _logger.LogError("key-metricsTTM error ex", makeRequestTTM.ErrorException);
                    _logger.LogError("key-metricsTTM error con", makeRequestTTM.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock key-metricsTTM" };
                    return response;
                }
                var resultTTM = JsonConvert.DeserializeObject<List<KeyMetricTTMRep>>(makeRequestTTM.Content);
                if (!resultTTM.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock key-metricsTTM is empty" };
                    return response;
                }
                var apiUrlRatioTTM = _baseUrl + $"stable/ratios-ttm?symbol={symbol}&limit=5";
                var makeRequestRatioTTM = await _apiClient.GetAsync<string>(apiUrlRatioTTM);
                if (!makeRequestRatioTTM.IsSuccessful)
                {
                    _logger.LogError("ratios-ttm error mess", makeRequestRatioTTM.ErrorMessage);
                    _logger.LogError("ratios-ttm error ex", makeRequestRatioTTM.ErrorException);
                    _logger.LogError("ratios-ttm error con", makeRequestRatioTTM.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock ratios-ttm" };
                    return response;
                }
                var resultRatioTTM = JsonConvert.DeserializeObject<List<RatioTTMResp>>(makeRequestRatioTTM.Content);
                if (!resultRatioTTM.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock ratios-ttm is empty" };
                    return response;
                }


                var apiUrlIcomeTTM = _baseUrl + $"stable/income-statement-ttm?symbol={symbol}&period={period}&limit=5";
                var makeRequestIncomeTTM = await _apiClient.GetAsync<string>(apiUrlIcomeTTM);
                if (!makeRequestIncomeTTM.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncomeTTM.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncomeTTM.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncomeTTM.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncomeTTM = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncomeTTM.Content);



                var apiUrlIcomeNo = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}&limit=11";
                var makeRequestIncomeNo = await _apiClient.GetAsync<string>(apiUrlIcomeNo);
                if (!makeRequestIncomeNo.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncomeNo.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncomeNo.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncomeNo.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncomeNo = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncomeNo.Content);


                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement-ttm?symbol={symbol}&period={period}&limit=5";
                var makeRequestCash = await _apiClient.GetAsync<string>(apiUrlCash);
                if (!makeRequestCash.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }


                var resultCashTTM = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCash.Content);

                var apiUrlCashNO = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}&limit=11";
                var makeRequestCashNo = await _apiClient.GetAsync<string>(apiUrlCashNO);
                if (!makeRequestCashNo.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCashNo.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCashNo.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCashNo.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }


                var resultCashNo = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCashNo.Content);

                var apiUrlGrowth = _baseUrl + $"stable/income-statement-growth?symbol={symbol}&period={period}&limit=10";
                var makeGrowthRequest = await _apiClient.GetAsync<string>(apiUrlGrowth);
                if (!makeGrowthRequest.IsSuccessful)
                {
                    _logger.LogError("stock income-growth error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock income-growth error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock  income-growth error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }
                var resultGrowth = JsonConvert.DeserializeObject<List<FinancialGrowth>>(makeGrowthRequest.Content);


                var getStockAnalyzer = await StockAnalyserRequest(symbol, period);
                if (getStockAnalyzer.StatusCode != 200)
                {

                    response.StatusCode = getStockAnalyzer.StatusCode;
                    response.DisplayMessage = getStockAnalyzer.DisplayMessage;
                    response.ErrorMessages = getStockAnalyzer.ErrorMessages;
                    return response;
                }

                var balanceSheetTMM = await GetStockBalanceSheetTTM(symbol, period);
                if (balanceSheetTMM.StatusCode != 200)
                {
                    response.StatusCode = balanceSheetTMM.StatusCode;
                    response.DisplayMessage = balanceSheetTMM.DisplayMessage;
                    response.ErrorMessages = balanceSheetTMM.ErrorMessages;
                    return response;
                }


                var marketCap = (getQuote.Result[0].price * (resultIncomeTTM[0].WeightedAverageShsOutDil));
                metricFirst.MarketCap = marketCap.ToString();

                //result[0].MarketCap.ToString();
                metricFirst.ProfitMarginTTM = resultRatioTTM[0].NetProfitMarginTTM.ToString() + "%";
                metricFirst.RevenueTTM = resultIncomeTTM[0].Revenue.ToString();
                metricFirst.NetIcomeTTM = resultIncomeTTM[0].NetIncome.ToString();
                if (!(resultIncomeNo.Count < 5))
                {
                metricFirst.AvgProfitMargin5yrs = ((resultRatio[0].NetProfitMargin + resultRatio[1].NetProfitMargin + resultRatio[2].NetProfitMargin + resultRatio[3].NetProfitMargin + resultRatio[4].NetProfitMargin) / 5).ToString() + "%";
                    var netincome5yearsAvg = ((resultIncomeNo[0].NetIncome + resultIncomeNo[1].NetIncome + resultIncomeNo[2].NetIncome + resultIncomeNo[3].NetIncome + resultIncomeNo[4].NetIncome) / 5);
                    metricFirst.NetIcomeTTM5year = netincome5yearsAvg.ToString();
                    metricFirst.PToEAvgNetIncomeFive5yrs = (marketCap / netincome5yearsAvg).ToString();
                }

                var pt = (marketCap / resultIncomeTTM[0].NetIncome);
                metricFirst.PToERatioTTM = $"{pt:F2}";
                //resultRatioTTM[0].PriceToEarningsRatioTTM.ToString();
                var ps = marketCap / resultIncomeTTM[0].Revenue;
                metricFirst.PSRatioTTM = $"{ps:F2}";

                metricFirst.GrossProfitMarginTTM = ((resultIncomeTTM[0].Revenue - resultIncomeTTM[0].CostOfRevenue)
                    / resultIncomeTTM[0].Revenue).ToString() + "%";
                //resultRatioTTM[0].GrossProfitMarginTTM.ToString() + "%";

                metricSecond.FreeCashFlow = resultCashTTM[0].FreeCashFlow.ToString();
                if (!(resultCashNo.Count < 5))
                {
                    metricSecond.AvgFCF5Yrs = ((resultCashNo[0].FreeCashFlow + resultCashNo[1].FreeCashFlow +
                        resultCashNo[2].FreeCashFlow + resultCashNo[3].FreeCashFlow + resultCashNo[4].FreeCashFlow) / 5).ToString();

                }
                var pf = marketCap / resultCashTTM[0].FreeCashFlow;
                metricSecond.PriceToFCFTTM = $"{pf:F2}";


                metricSecond.EnterpriseValue = result[0].EnterpriseValue.ToString();
                metricSecond.EVToNet = (result[0].EnterpriseValue / resultIncomeTTM[0].NetIncome).ToString();

                metricSecond.EVToFCF = (result[0].EnterpriseValue / resultCashTTM[0].FreeCashFlow).ToString();
                metricSecond.DividendsYieldTTM = (resultCashTTM[0].CommonDividendsPaid / marketCap).ToString() + "%";
                metricSecond.DividendsPaidTTM = (resultCashTTM[0].CommonDividendsPaid).ToString();




                metricThird.ReturnOnAsset = (resultIncomeTTM[0].NetIncome / balanceSheetTMM.Result[0].TotalAssets).ToString() + "%";
                metricThird.ReturnOnEquity = (resultCashTTM[0].FreeCashFlow / balanceSheetTMM.Result[0].TotalEquity).ToString() + "%";
                if ((resultIncomeNo.Count > 2))
                {
                    metricThird.CompRevGrowth3yrs = (Math.Pow((double)resultIncomeNo[0].Revenue / (double)resultIncomeNo[2].Revenue, 1.0 / 3.0) - 1).ToString() + "%";
                }
                 if ((resultIncomeNo.Count > 4))
                {
                    metricThird.CompRevGrowth5yrs = (Math.Pow((double)resultIncomeNo[0].Revenue / (double)resultIncomeNo[4].Revenue, 1.0 / 5.0) - 1).ToString() + "%";
                }
                if ((resultIncomeNo.Count > 9))
                {
                    metricThird.CompRevGrowth10yrs = (Math.Pow((double)resultIncomeNo[0].Revenue / (double)resultIncomeNo[9].Revenue, 1.0 / 10.0) - 1).ToString() + "%";
                }


                var pb = marketCap / balanceSheetTMM.Result[0].TotalEquity;
                metricThird.PriceToBookRatio = $"{pb:F2}";
                metricThird.ReturnOnInvestedCapitalTTM = getStockAnalyzer.Result.ROIC.First;
                if(!string.IsNullOrEmpty(getStockAnalyzer.Result.ROIC.Fifth))
                {
                    metricThird.AvgROIC5yrs = getStockAnalyzer.Result.ROIC.Fifth;
                }
               




                metricFourth.AYearHigh = getQuote.Result[0].yearHigh.ToString();
                metricFourth.AYearlow = getQuote.Result[0].yearLow.ToString();

                var resultFinal = new FundamentalMetricData();

                resultFinal.metricFirst = metricFirst;
                resultFinal.metricSecond = metricSecond;
                resultFinal.metricThird = metricThird;
                resultFinal.metricFourth = metricFourth;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resultFinal;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"metric ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock metric at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<StockAnalyserResponse>> StockAnalyserRequest(string symbol, string period)
        {
            var response = new ResponseDto<StockAnalyserResponse>();

            try
            {
                var getQuote = await GetStockQuote(symbol);
                if (getQuote.StatusCode != 200)
                {
                    response.StatusCode = getQuote.StatusCode;
                    response.DisplayMessage = getQuote.DisplayMessage;
                    response.ErrorMessages = getQuote.ErrorMessages;
                    return response;
                }

                var apiUrlBalance = _baseUrl + $"stable/balance-sheet-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestBalance = await _apiClient.GetAsync<string>(apiUrlBalance);
                if (!makeRequestBalance.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequestBalance.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequestBalance.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequestBalance.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var resultBalance = JsonConvert.DeserializeObject<List<BalanceSheetResp>>(makeRequestBalance.Content);
                if (!resultBalance.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock balance sheet statement is empty" };
                    return response;
                }

                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestCash = await _apiClient.GetAsync<string>(apiUrlCash);
                if (!makeRequestCash.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }


                var resultCash = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCash.Content);
                if (!resultCash.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock cash flow statement is empty" };
                    return response;
                }

                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestIncome = await _apiClient.GetAsync<string>(apiUrlIcome);
                if (!makeRequestIncome.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome.Content);
                if (!resultIncome.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Income statement is empty" };
                    return response;
                }


                var latestPrice = getQuote.Result[0].price;
                var latestIncomeStatement = resultIncome[0];
                var latestBalanceSheet = resultBalance[0];
                var latestCashFlow = resultCash[0];



                var netIncome = latestIncomeStatement.NetIncome;
                var revenue = latestIncomeStatement.Revenue;



                var fcfMargins = new List<double>();
                var roics = new List<double>();
                int count = Math.Min(Math.Min(resultIncome.Count, resultBalance.Count), resultCash.Count);

                for (int i = 0; i < resultIncome.Count && i < 10; i++)
                {
                    var fcf = CalculateFCF((long)resultCash[i].OperatingCashFlow, (long)resultCash[i].CapitalExpenditure);
                    var fcfMargin = CalculateFCFMargin(fcf, (double)resultIncome[i].Revenue);
                    var roic = CalculateROIC((double)resultIncome[i].NetIncome, (double)resultBalance[i].TotalDebt, (double)resultBalance[i].CashAndCashEquivalents, (double)resultBalance[i].TotalEquity);

                    fcfMargins.Add(fcfMargin);
                    roics.Add(roic);
                }

                var pe = CalculatePE(getQuote.Result[0].marketCap, (double)resultIncome[0].NetIncome);
                var pfcf = CalculatePFCF(getQuote.Result[0].marketCap, CalculateFCF((long)resultCash[0].OperatingCashFlow, (long)resultCash[0].CapitalExpenditure));




                double profitMargin = (double)(netIncome / revenue);
                double revenueGrowth = CalculateYoYGrowth((double)resultIncome[0].Revenue, (double)resultIncome[1].Revenue);

                var ROIC = new ROIC();
                var RevenueGrowth = new RevenueGrowth();
                var ProfitMargin = new ProfitMargin();
                var FreeCashFlowMargin = new FreeCashFlowMargin();
                var PERatio = new PERatio();
                var PFCF = new PFCF();

                ROIC.First = $"{roics[0]:F2}%";
                RevenueGrowth.First = $"{revenueGrowth:F2}" + "%";
                ProfitMargin.First = (profitMargin * 100).ToString() + "%";
                FreeCashFlowMargin.First = $"{fcfMargins[0]:F2}%";
                PERatio.First = $"{pe:F2}";
                PFCF.First = $"{pfcf:F2}";


                if (count > 4)
                {

                    var latestPrice1 = getQuote.Result[0].price;
                    var latestIncomeStatement1 = resultIncome[4];
                    var latestBalanceSheet1 = resultBalance[4];
                    var latestCashFlow1 = resultCash[4];


                    var netIncome1 = latestIncomeStatement1.NetIncome;
                    var revenue1 = latestIncomeStatement1.Revenue;



                    double profitMargin1 = (double)(netIncome1 / revenue1);
                    double revenueGrowth1 = CalculateCAGR((double)resultIncome[0].Revenue, (double)resultIncome[4].Revenue, 5);
              


                ROIC.Fifth = $"{roics.Take(5).Average():F2}%";
                RevenueGrowth.Fifth = $"{revenueGrowth1:F2}" + "%"; ;
                ProfitMargin.Fifth = (profitMargin1 * 100).ToString();
                FreeCashFlowMargin.Fifth = $"{fcfMargins.Take(5).Average():F2}%";

                }
                if (count > 9)
                {

                    var latestPrice2 = getQuote.Result[0].price;
                    var latestIncomeStatement2 = resultIncome[9];
                    var latestBalanceSheet2 = resultBalance[9];
                    var latestCashFlow2 = resultCash[9];


                    var netIncome2 = latestIncomeStatement2.NetIncome;
                    var revenue2 = latestIncomeStatement2.Revenue;


                    double profitMargin2 = (double)(netIncome2 / revenue2);
                    double revenueGrowth2 = CalculateCAGR((double)resultIncome[0].Revenue, (double)resultIncome[9].Revenue, 10);


                    ROIC.Ten = $"{roics.Take(10).Average():F2}%";
                    RevenueGrowth.Ten = $"{revenueGrowth2:F2}" + "%";
                    ProfitMargin.Ten = (profitMargin2 * 100).ToString();
                    FreeCashFlowMargin.Ten = $"{fcfMargins.Take(10).Average():F2}%";
                }

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = new StockAnalyserResponse()
                {
                    ROIC = ROIC,
                    RevGrowth = RevenueGrowth,
                    PERatio = PERatio,
                    FreeCashFlowMargin = FreeCashFlowMargin,
                    PFCF = PFCF,
                    ProfitMargin = ProfitMargin,
                };
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"metric ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock analy er stats at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<IEnumerable<Alpha8PillerResp>>> AlphaStock8Pillers(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<Alpha8PillerResp>>();
            try
            {
                var resp = new List<Alpha8PillerResp>();
                var getQuote = await GetStockQuote(symbol);
                if (getQuote.StatusCode != 200)
                {
                    response.StatusCode = getQuote.StatusCode;
                    response.DisplayMessage = getQuote.DisplayMessage;
                    response.ErrorMessages = getQuote.ErrorMessages;
                    return response;
                }
                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}&limit=5";
                var makeRequestIncome = await _apiClient.GetAsync<string>(apiUrlIcome);
                if (!makeRequestIncome.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome.Content);

                var resultIncomeTTM = await GetStockIncomeStatementTTM(symbol, period);
                if (resultIncomeTTM.StatusCode != 200)
                {
                    response.StatusCode = resultIncomeTTM.StatusCode;
                    response.DisplayMessage = resultIncomeTTM.DisplayMessage;
                    response.ErrorMessages = resultIncomeTTM.ErrorMessages;
                    return response;
                }


                var apiUrlKey = _baseUrl + $"stable/key-metrics?symbol={symbol}&limit=5&period={period}";
                var makeRequest = await _apiClient.GetAsync<string>(apiUrlKey);
                if (!makeRequest.IsSuccessful)
                {
                    _logger.LogError("key-metrics error mess", makeRequest.ErrorMessage);
                    _logger.LogError("key-metrics error ex", makeRequest.ErrorException);
                    _logger.LogError("key-metrics error con", makeRequest.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock key-metrics" };
                    return response;
                }
                var resultKey = JsonConvert.DeserializeObject<List<KeyMetricRes>>(makeRequest.Content);
                if (!resultKey.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock key-metrics is empty" };
                    return response;
                }

                var marketCap = (getQuote.Result[0].price * (resultIncomeTTM.Result[0].WeightedAverageShsOutDil));
                var netincome5yearsAvg = ((resultIncome[0].NetIncome + resultIncome[1].NetIncome + resultIncome[2].NetIncome + resultIncome[3].NetIncome + resultIncome[4].NetIncome) / 5);
                var avgPe = marketCap / netincome5yearsAvg;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "P/E Avg Net Income (5 yr) < 22",
                    amount = FormatNumber((double)avgPe),
                    isActive = Compare("22", (double)avgPe, ">")
                });
                var diffIncome = resultIncome[0].NetIncome - resultIncome[4].NetIncome;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "Net Income Growth (5 yr)",
                    amount = FormatNumber((double)diffIncome),
                    isActive = Compare(resultIncome[0].NetIncome.ToString(), (double)resultIncome[4].NetIncome, ">")
                });
                var getStockAnalyzer = await StockAnalyserRequest(symbol, period);
                if (getStockAnalyzer.StatusCode != 200)
                {

                    response.StatusCode = getStockAnalyzer.StatusCode;
                    response.DisplayMessage = getStockAnalyzer.DisplayMessage;
                    response.ErrorMessages = getStockAnalyzer.ErrorMessages;
                    return response;
                }
                resp.Add(new Alpha8PillerResp()
                {
                    header = "Avg ROIC (5 yr) > 10%",
                    amount = getStockAnalyzer.Result.ROIC.Fifth,
                    isActive = Compare2(getStockAnalyzer.Result.ROIC.Fifth, "10%", ">")
                });
                var difReveG = resultIncome[0].Revenue - resultIncome[4].Revenue;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "Revenue Growth (5 yr)",
                    amount =FormatNumber((double)difReveG),
                    isActive = Compare2(resultIncome[0].Revenue.ToString(), resultIncome[4].Revenue.ToString(), ">")
                });

                var apiUrlIcome2 = _baseUrl + $"stable/income-statement-ttm?symbol={symbol}&period={period}&limit=5";
                var makeRequestIncome2 = await _apiClient.GetAsync<string>(apiUrlIcome2);
                if (!makeRequestIncome2.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome2.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome2.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome2.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome2 = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome2.Content);
                var weighted = ((resultIncome[0].WeightedAverageShsOutDil / resultIncome[4].WeightedAverageShsOutDil) - 1.0) * 100;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "Shares Outstanding Decrease (5 yr)",
                    amount = FormatNumber(double.Parse(weighted.ToString()))+"%",
                    isActive = Compare(resultIncome2[0].WeightedAverageShsOutDil.ToString(), double.Parse(resultIncome[0].WeightedAverageShsOutDil.ToString()), "<")
                });

                var apiUrlBalanceTtm = _baseUrl + $"stable/balance-sheet-statement-ttm?symbol={symbol}&period={period}&limit=10";
                var makeRequestBalanceTTm = await _apiClient.GetAsync<string>(apiUrlBalanceTtm);
                if (!makeRequestBalanceTTm.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequestBalanceTTm.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequestBalanceTTm.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequestBalanceTTm.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var resultBalanceTTM = JsonConvert.DeserializeObject<List<BalanceSheetResp>>(makeRequestBalanceTTm.Content);
                var cashflow = await GetStockCashFlowStatement(symbol, period, "5");
                if (cashflow.StatusCode != 200)
                {

                    response.StatusCode = cashflow.StatusCode;
                    response.DisplayMessage = cashflow.DisplayMessage;
                    response.ErrorMessages = cashflow.ErrorMessages;
                    return response;
                }
                var avgCashflow = ((cashflow.Result[0].FreeCashFlow +
                    cashflow.Result[1].FreeCashFlow +
                    cashflow.Result[2].FreeCashFlow +
                    cashflow.Result[3].FreeCashFlow +
                    cashflow.Result[4].FreeCashFlow) / 5);
                var ltl = resultBalanceTTM[0].TotalCurrentLiabilities / avgCashflow;

                var cashTTM = await GetStockCashFlowStatementTTM(symbol, period);
                if (cashTTM.StatusCode != 200)
                {
                    response.StatusCode = cashTTM.StatusCode;
                    response.DisplayMessage = cashTTM.DisplayMessage;
                    response.ErrorMessages = cashTTM.ErrorMessages;
                    return response;
                }

                var diff = resultBalanceTTM[0].TotalCurrentLiabilities / cashTTM.Result[0].FreeCashFlow;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "LTL/Avg FCF (5 yr) < 5",
                    amount = FormatNumber((double)diff),
                    isActive = Compare(ltl.ToString(), 5, "<")
                });
                var diffFCF = cashflow.Result[0].FreeCashFlow -  cashflow.Result[4].FreeCashFlow;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "FCF Growth (5 yr)",
                    amount = FormatNumber((double)diffFCF),
                    isActive = Compare(cashflow.Result[0].FreeCashFlow.ToString(), (double)cashflow.Result[4].FreeCashFlow, ">")
                });


                var avgFCF = marketCap / avgCashflow;
                resp.Add(new Alpha8PillerResp()
                {
                    header = "P/Avg FCF (5 yr) < 22",
                    amount = FormatNumber((double)avgFCF),
                    isActive = Compare("22", (double)avgFCF, ">")
                });


                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = resp;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Get Alpha 8 piller service not available" };
                response.StatusCode = 400;
                return response;
            }
        }




        public async Task<ResponseDto<StockAlphaResp>> StockAlphaRequest(string symbol, string period)
        {
            var response = new ResponseDto<StockAlphaResp>();

            try
            {
                var getQuote = await GetStockQuote(symbol);
                if (getQuote.StatusCode != 200)
                {
                    response.StatusCode = getQuote.StatusCode;
                    response.DisplayMessage = getQuote.DisplayMessage;
                    response.ErrorMessages = getQuote.ErrorMessages;
                    return response;
                }

                var apiUrlBalance = _baseUrl + $"stable/balance-sheet-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestBalance = await _apiClient.GetAsync<string>(apiUrlBalance);
                if (!makeRequestBalance.IsSuccessful)
                {
                    _logger.LogError("stock balance-sheet-statement error mess", makeRequestBalance.ErrorMessage);
                    _logger.LogError("stock balance-sheet-statement error ex", makeRequestBalance.ErrorException);
                    _logger.LogError("stock balance-sheet-statement error con", makeRequestBalance.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock balance sheet statement" };
                    return response;
                }
                var resultBalance = JsonConvert.DeserializeObject<List<BalanceSheetResp>>(makeRequestBalance.Content);
                if (!resultBalance.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock balance sheet statement is empty" };
                    return response;
                }

                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestCash = await _apiClient.GetAsync<string>(apiUrlCash);
                if (!makeRequestCash.IsSuccessful)
                {
                    _logger.LogError("stock cash-flow-statement error mess", makeRequestCash.ErrorMessage);
                    _logger.LogError("stock cash-flow-statement error ex", makeRequestCash.ErrorException);
                    _logger.LogError("stock cash-flow-statement error con", makeRequestCash.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock cash flow statement" };
                    return response;
                }


                var resultCash = JsonConvert.DeserializeObject<List<CashFlowStatement>>(makeRequestCash.Content);
                if (!resultCash.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock cash flow statement is empty" };
                    return response;
                }

                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}&limit=10";
                var makeRequestIncome = await _apiClient.GetAsync<string>(apiUrlIcome);
                if (!makeRequestIncome.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome.Content);
                if (!resultIncome.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Income statement is empty" };
                    return response;
                }


                var latestPrice = getQuote.Result[0].price;
                var latestIncomeStatement = resultIncome[0];
                var latestBalanceSheet = resultBalance[0];
                var latestCashFlow = resultCash[0];



                var netIncome = latestIncomeStatement.NetIncome;
                var revenue = latestIncomeStatement.Revenue;



                var fcfMargins = new List<double>();
                var roics = new List<double>();

                for (int i = 0; i < resultIncome.Count && i < 10; i++)
                {
                    var fcf = CalculateFCF((long)resultCash[i].OperatingCashFlow, (long)resultCash[i].CapitalExpenditure);
                    var fcfMargin = CalculateFCFMargin(fcf, (double)resultIncome[i].Revenue);
                    var roic = CalculateROIC((double)resultIncome[i].NetIncome, (double)resultBalance[i].TotalDebt, (double)resultBalance[i].CashAndCashEquivalents, (double)resultBalance[i].TotalEquity);

                    fcfMargins.Add(fcfMargin);
                    roics.Add(roic);
                }

                var pe = CalculatePE(getQuote.Result[0].marketCap, (double)resultIncome[0].NetIncome);
                var pfcf = CalculatePFCF(getQuote.Result[0].marketCap, CalculateFCF((long)resultCash[0].OperatingCashFlow, (long)resultCash[0].CapitalExpenditure));
                double profitMargin = (double)(netIncome / revenue);
                double revenueGrowth = CalculateYoYGrowth((double)resultIncome[0].Revenue, (double)resultIncome[1].Revenue);

                var ROIC = new ROIC();
                var RevenueGrowth = new RevenueGrowth();
                var ProfitMargin = new ProfitMargin();
                var FreeCashFlowMargin = new FreeCashFlowMargin();
                var PERatio = new PERatio();
                var PFCF = new PFCF();
                var Weighted = new WeightedAverageShsOut();
                var NetIcome = new NetIncomeAlpha();



                Weighted.First = $"{latestIncomeStatement.WeightedAverageShsOut:F2}";
                NetIcome.First = $"{latestIncomeStatement.NetIncome:F2}";
                ROIC.First = $"{roics[0]:F2}%";
                RevenueGrowth.First = revenueGrowth.ToString() + "%";
                ProfitMargin.First = (profitMargin * 100).ToString() + "%";
                FreeCashFlowMargin.First = $"{fcfMargins[0]:F2}%";
                PERatio.First = $"{pe:F2}";
                PFCF.First = $"{pfcf:F2}";



                var latestPrice1 = getQuote.Result[0].price;
                var latestIncomeStatement1 = resultIncome[4];
                var latestBalanceSheet1 = resultBalance[4];
                var latestCashFlow1 = resultCash[4];


                var netIncome1 = latestIncomeStatement1.NetIncome;
                var revenue1 = latestIncomeStatement1.Revenue;



                double profitMargin1 = (double)(netIncome1 / revenue1);
                double revenueGrowth1 = CalculateCAGR((double)resultIncome[0].Revenue, (double)resultIncome[4].Revenue, 5);

                Weighted.Fifth = $"{latestIncomeStatement1.WeightedAverageShsOut:F2}";
                NetIcome.Fifth = $"{latestIncomeStatement1.NetIncome:F2}";
                ROIC.Fifth = $"{roics.Take(5).Average():F2}%";
                RevenueGrowth.Fifth = revenueGrowth1.ToString();
                ProfitMargin.Fifth = (profitMargin1 * 100).ToString();
                FreeCashFlowMargin.Fifth = $"{fcfMargins.Take(5).Average():F2}%";



                var latestPrice2 = getQuote.Result[0].price;
                var latestIncomeStatement2 = resultIncome[9];
                var latestBalanceSheet2 = resultBalance[9];
                var latestCashFlow2 = resultCash[9];


                var netIncome2 = latestIncomeStatement2.NetIncome;
                var revenue2 = latestIncomeStatement2.Revenue;


                double profitMargin2 = (double)(netIncome2 / revenue2);
                double revenueGrowth2 = CalculateCAGR((double)resultIncome[0].Revenue, (double)resultIncome[9].Revenue, 10);

                Weighted.Ten = $"{latestIncomeStatement2.WeightedAverageShsOut:F2}";
                NetIcome.Ten = $"{latestIncomeStatement2.NetIncome:F2}";
                ROIC.Ten = $"{roics.Take(10).Average():F2}%";
                RevenueGrowth.Ten = revenueGrowth2.ToString();
                ProfitMargin.Ten = (profitMargin2 * 100).ToString();
                FreeCashFlowMargin.Ten = $"{fcfMargins.Take(10).Average():F2}%";


                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = new StockAlphaResp()
                {
                    ROIC = ROIC,
                    RevGrowth = RevenueGrowth,
                    PERatio = PERatio,
                    FreeCashFlowMargin = FreeCashFlowMargin,
                    PFCF = PFCF,
                    ProfitMargin = ProfitMargin,
                    MarketCap = $"{getQuote.Result[0].marketCap:F2}",
                    AverageShareOutstanding = Weighted,
                    NetIcome = NetIcome,


                };
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"metric ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to get the stock alpha stats at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        private double CalculateYoYGrowth(double current, double previous)
        {
            return ((double)(current - previous) / previous) * 100;
        }

        private double CalculateCAGR(double endValue, double startValue, int years)
        {
            return (Math.Pow((double)endValue / startValue, 1.0 / years) - 1) * 100;
        }
        public double CalculateFCF(long operatingCashFlow, long capex)
    => operatingCashFlow - Math.Abs(capex); // CapEx is negative in FMP data

        public double CalculateFCFMargin(double fcf, double revenue)
            => (double)fcf / revenue * 100;

        public double CalculateROIC(double netIncome, double totalDebt, double cash, double equity)
        {
            var investedCapital = totalDebt + equity - cash;
            return (double)netIncome / investedCapital * 100;
        }

        public double CalculatePE(double marketCap, double netIncome)
            => marketCap / netIncome;

        public double CalculatePFCF(double marketCap, double fcf)
            => marketCap / fcf;

        public bool Compare(string formattedValue, double rawValue, string comparison)
        {
            double parsedFormattedValue = ParseFormattedValue(formattedValue);

            return comparison switch
            {
                ">" => parsedFormattedValue > rawValue,
                "<" => parsedFormattedValue < rawValue,
                ">=" => parsedFormattedValue >= rawValue,
                "<=" => parsedFormattedValue <= rawValue,
                "==" => parsedFormattedValue == rawValue,
                "!=" => parsedFormattedValue != rawValue,
                _ => throw new ArgumentException("Invalid comparison operator. Use: >, <, >=, <=, ==, !=")
            };
        }
        public bool Compare2(string formattedValue, string rawValueStr, string comparison)
        {
            double leftValue = ParseFormattedValue2(formattedValue);
            double rightValue = ParseFormattedValue2(rawValueStr);

            return comparison switch
            {
                ">" => leftValue > rightValue,
                "<" => leftValue < rightValue,
                ">=" => leftValue >= rightValue,
                "<=" => leftValue <= rightValue,
                "==" => leftValue == rightValue,
                "!=" => leftValue != rightValue,
                _ => throw new ArgumentException("Invalid comparison operator. Use: >, <, >=, <=, ==, !=")
            };
        }

        private double ParseFormattedValue2(string value)
        {
            value = value.Trim();

            if (value.EndsWith("%"))
            {
                value = value.TrimEnd('%');

                if (double.TryParse(value, out double percentValue))
                {
                    return percentValue / 100.0; // convert to decimal
                }
            }
            else
            {
                if (double.TryParse(value, out double numericValue))
                {
                    return numericValue;
                }
            }

            throw new FormatException($"Unable to parse '{value}' as a numeric or percentage value.");
        }

        private double ParseFormattedValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty");

            value = value.Trim().ToUpper();

            double multiplier = 1;
            if (value.EndsWith("T"))
            {
                multiplier = 1_000_000_000_000;
                value = value[..^1];
            }
            else if (value.EndsWith("B"))
            {
                multiplier = 1_000_000_000;
                value = value[..^1];
            }
            else if (value.EndsWith("M"))
            {
                multiplier = 1_000_000;
                value = value[..^1];
            }
            else if (value.EndsWith("K"))
            {
                multiplier = 1_000;
                value = value[..^1];
            }

            if (!double.TryParse(value, out double baseValue))
                throw new FormatException("Invalid formatted number");

            return baseValue * multiplier;
        }
        public string FormatNumber(double number)
        {
            string suffix;
            double formatted;

            if (number >= 1_000_000_000_000)
            {
                formatted = number / 1_000_000_000_000.0;
                suffix = "T";
            }
            else if (number >= 1_000_000_000)
            {
                formatted = number / 1_000_000_000.0;
                suffix = "B";
            }
            else if (number >= 1_000_000)
            {
                formatted = number / 1_000_000.0;
                suffix = "M";
            }
            else if (number >= 1_000)
            {
                formatted = number / 1_000.0;
                suffix = "K";
            }
            else
            {
                return number.ToString("N2"); // Less than 1000, just format to two decimals
            }

            return $"{formatted:N2}{suffix}";
        }

        public async Task<ResponseDto<StockAnaResponse>> StockAnalyserResponse(StockAnalyserRequest req)
        {
            var response = new ResponseDto<StockAnaResponse>();

            try
            {
                var DiscountedEarningsValue = new DiscountedEarningsValue();
                var DiscountedFreeCashFlowValue = new DiscountedFreeCashFlowValue();

                var apiUrlIcome = _baseUrl + $"stable/income-statement-ttm?symbol={req.symbol}&period=annual&limit=1";
                var makeRequestIncome = await _apiClient.GetAsync<string>(apiUrlIcome);
                if (!makeRequestIncome.IsSuccessful)
                {
                    _logger.LogError("stock income statement error mess", makeRequestIncome.ErrorMessage);
                    _logger.LogError("stock income statement error ex", makeRequestIncome.ErrorException);
                    _logger.LogError("stock income statement error con", makeRequestIncome.Content);
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Unable to get the stock Income statement" };
                    return response;
                }
                var resultIncome = JsonConvert.DeserializeObject<List<IncomeStatementResp>>(makeRequestIncome.Content);
                if (!resultIncome.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock Income statement is empty" };
                    return response;
                }
                var result = new StockAnaResponse();

                double AverageDiluted = (double)resultIncome[0].WeightedAverageShsOutDil;
                var latestIncomeStatementRevenue = resultIncome[0].Revenue;

                double futureRevLow = (double)(latestIncomeStatementRevenue * Math.Pow(1 + (req.RevGrowth.low / 100), req.years));
                double futureNetIcomeLow = futureRevLow * (req.ProfitMargin.low / 100);
                double futureMarketCapLow = futureNetIcomeLow * (req.PERatio.low);
                double presentValueMarketCapLow = futureMarketCapLow / Math.Pow(1 + (req.DesiredAnnReturn.low / 100), req.years);
                double presentValuePricePerShareLow = presentValueMarketCapLow / AverageDiluted;


                double futureFreeCashFlowLow = futureRevLow * (req.FreeCashFlowMargin.low / 100);
                double futureMarketCapCashFlowLow = futureFreeCashFlowLow * (req.PFCF.low);
                double presentValueMarketCapCashFlowLow = futureMarketCapCashFlowLow / Math.Pow(1 + (req.DesiredAnnReturn.low / 100), req.years);
                double presentValuePricePerShareCashFlowLow = presentValueMarketCapCashFlowLow / AverageDiluted;

                if (req.selection == 1)
                {
                    DiscountedEarningsValue.Low = presentValuePricePerShareLow;
                    DiscountedFreeCashFlowValue.Low = presentValuePricePerShareCashFlowLow;
                    result.DiscountedEarningsValue = DiscountedEarningsValue;
                    result.DiscountedFreeCashFlowValue = DiscountedFreeCashFlowValue;
                    response.Result = result;
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    return response;
                }




                double? futureRevMid = (latestIncomeStatementRevenue * Math.Pow((double)(1 + (req.RevGrowth.mid / 100)), req.years));
                double? futureNetIcomeMid = futureRevMid * (req.ProfitMargin.mid / 100);
                double? futureMarketCapMid = futureNetIcomeMid * (req.PERatio.mid);
                double? presentValueMarketCapMid = futureMarketCapMid / Math.Pow((double)(1 + (req.DesiredAnnReturn.mid / 100)), req.years);
                double? presentValuePricePerShareMid = presentValueMarketCapMid / AverageDiluted;
                double? futureFreeCashFlowMid = futureRevMid * (req.FreeCashFlowMargin.mid / 100);
                double? futurMarketCapCashFlowMid = futureFreeCashFlowMid * (req.PFCF.mid);
                double? presentValueMarketCapCashFlowMid = futurMarketCapCashFlowMid / Math.Pow((double)(1 + (req.DesiredAnnReturn.mid / 100)), req.years);
                double? presentValuePricePerShareCashFlowMid = presentValueMarketCapCashFlowMid / AverageDiluted;


                if (req.selection == 2)
                {
                    DiscountedEarningsValue.Low = presentValuePricePerShareLow;
                    DiscountedEarningsValue.Mid = presentValuePricePerShareMid;
                    DiscountedFreeCashFlowValue.Low = presentValuePricePerShareCashFlowLow;
                    DiscountedFreeCashFlowValue.Mid = presentValuePricePerShareCashFlowMid;
                    result.DiscountedEarningsValue = DiscountedEarningsValue;
                    result.DiscountedFreeCashFlowValue = DiscountedFreeCashFlowValue;
                    response.Result = result;
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    return response;
                }




                double? futureRevHigh = (latestIncomeStatementRevenue * Math.Pow((double)(1 + (req.RevGrowth.High / 100)), req.years));
                double? futureNetIcomeHigh = futureRevHigh * (req.ProfitMargin.High / 100);
                double? futureMarketCapHigh = futureNetIcomeHigh * (req.PERatio.High);
                double? presentValueMarketCapHigh = futureMarketCapHigh / Math.Pow((double)(1 + (req.DesiredAnnReturn.High / 100)), req.years);
                double? presentValuePricePerShareHigh = presentValueMarketCapHigh / AverageDiluted;


                double? futureFreeCashFlowHigh = futureRevHigh * (req.FreeCashFlowMargin.High / 100);
                double? futureMarketCapCashFlowHigh = futureFreeCashFlowHigh * (req.PFCF.High);
                double? presentValueMarketCapCashFlowHigh = futureMarketCapCashFlowHigh / Math.Pow((double)(1 + (req.DesiredAnnReturn.High / 100)), req.years);
                double? presentValuePricePerShareCashFlowHigh = presentValueMarketCapCashFlowHigh / AverageDiluted;

                if (req.selection == 3)
                {
                    DiscountedEarningsValue.Low = presentValuePricePerShareLow;
                    DiscountedEarningsValue.Mid = presentValuePricePerShareMid;
                    DiscountedEarningsValue.High = presentValuePricePerShareHigh;
                    DiscountedFreeCashFlowValue.Low = presentValuePricePerShareCashFlowLow;
                    DiscountedFreeCashFlowValue.Mid = presentValuePricePerShareCashFlowMid;
                    DiscountedFreeCashFlowValue.High = presentValuePricePerShareCashFlowHigh;
                    result.DiscountedEarningsValue = DiscountedEarningsValue;
                    result.DiscountedFreeCashFlowValue = DiscountedFreeCashFlowValue;
                    response.Result = result;
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    return response;
                }





                result.DiscountedFreeCashFlowValue = DiscountedFreeCashFlowValue;
                response.Result = result;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"stock analysis predit ex - {ex.Message}", ex);
                response.ErrorMessages = new List<string> { "Unable to formulate stock analysis at the moment" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }


        public async Task<ResponseDto<string>> AddMyAlpha(string userid, List<MyAlphaReq> req)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _userSavePillerRepo.GetQueryable().Where(u => u.Userid == userid).ToListAsync();
                if (check.Any())
                {
                    _userSavePillerRepo.Delete(check);
                    await _userSavePillerRepo.SaveChanges();
                }
                var saveData = new List<UserSavePiller>();
                foreach (var piller in req)
                {
                    saveData.Add(new UserSavePiller()
                    {
                        Comparison = piller.Comparison,
                        Format = piller.Format,
                        PillerName = piller.PillerName,
                        Userid = userid,
                        Value = piller.Value,
                    });
                }
                _userSavePillerRepo.AddRanges(saveData);

                await _userSavePillerRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "My Piller updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "My Piller service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<List<MyAlphaReq>>> RetrieveMyAlpha(string userid)
        {
            var response = new ResponseDto<List<MyAlphaReq>>();
            try
            {
                var check = await _userSavePillerRepo.GetQueryable().Where(u => u.Userid == userid).Select(u => new MyAlphaReq
                {
                    Comparison = u.Comparison,
                    Format = u.Format,
                    PillerName = u.PillerName,
                    Value = u.Value,
                }).ToListAsync();

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = check;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Retrieve My Piller service not available" };
                response.StatusCode = 400;
                return response;
            }
        }

    }
}
