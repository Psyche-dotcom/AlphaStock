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
        public StockService(ILogger<StockService> logger,
            IApiClient apiClient,
            IConfiguration configuration,
            IAlphaRepository<StockWishList> stockWishRepo)
        {
            _logger = logger;
            _apiClient = apiClient;
            _configuration = configuration;
            _baseUrl = _configuration["FMP:BASEURL"];
            _stockWishRepo = stockWishRepo;
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
        public async Task<ResponseDto<IEnumerable<IncomeStatementResp>>> GetStockIncomeStatement(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<IncomeStatementResp>>();
            try
            {

                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={symbol}&period={period}";
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
        public async Task<ResponseDto<IEnumerable<BalanceSheetResp>>> GetStockBalanceSheet(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<BalanceSheetResp>>();
            try
            {

                var apiUrlBalance = _baseUrl + $"stable/balance-sheet-statement?symbol={symbol}&period={period}";
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
        public async Task<ResponseDto<IEnumerable<CashFlowStatement>>> GetStockCashFlowStatement(string symbol, string period)
        {
            var response = new ResponseDto<IEnumerable<CashFlowStatement>>();
            try
            {

                var apiUrlCash = _baseUrl + $"stable/cash-flow-statement?symbol={symbol}&period={period}";
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
                var resultCash = JsonConvert.DeserializeObject<IEnumerable<CashFlowStatement>>(makeRequestCash.Content);
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

                var apiUrl = _baseUrl + $"stable/search-symbol?query={symbol}&limit=20";
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
                var resultRatioTTM = JsonConvert.DeserializeObject<IEnumerable<RatioTTMResp>>(makeRequestRatioTTM.Content);
                if (!resultRatioTTM.Any())
                {
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Stock ratios-ttm is empty" };
                    return response;
                }

                metricFirst.RevenuePerShare = resultRatio[0].RevenuePerShare.ToString();
                metricFirst.PToERatio = resultRatio[0].PriceToEarningsRatio.ToString();
                metricFirst.PToERatioFive5yrs = resultRatio[4].PriceToEarningsRatio.ToString();
                metricFirst.PSRatio = resultRatio[0].PriceToSalesRatio.ToString();
                metricFirst.PSRatioFive5yrs = resultRatio[4].PriceToSalesRatio.ToString();
                metricFirst.GrossProfitMargin = resultRatio[0].GrossProfitMargin.ToString();


                metricSecond.EnterpriseValue = result[0].EnterpriseValue.ToString();
                metricSecond.EV5YEARS = result[4].EnterpriseValue.ToString();
                metricSecond.EVToSales = result[0].EvToSales.ToString();
                metricSecond.EVToSales5yrs = result[4].EvToSales.ToString();
                metricSecond.EVToFCF5yrs = result[4].EvToFreeCashFlow.ToString();
                metricSecond.EVToFCF = result[0].EvToFreeCashFlow.ToString();
                metricSecond.FreeCashFlowYield = result[0].FreeCashFlowYield.ToString();
                metricSecond.FiveYearAvgFreeCashFlowYield = result[4].FreeCashFlowYield.ToString();
                metricSecond.DividendsYieldTTM = resultRatio[0].DividendYield.ToString();
                metricSecond.FCFTTMTOEQUITYTTM = resultTTM[0].FreeCashFlowToEquityTTM.ToString();



                metricThird.ReturnOnAsset = result[0].ReturnOnAssets.ToString();
                metricThird.ReturnOnEquity = result[0].ReturnOnEquity.ToString();
                metricThird.ReturnOnInvestedCapitalTTM = resultTTM[0].ReturnOnInvestedCapitalTTM.ToString();
                metricThird.ADayHigh = getQuote.Result[0].dayHigh.ToString();
                metricThird.ADaylow = getQuote.Result[0].dayLow.ToString();
                metricThird.AYearHigh = getQuote.Result[0].yearHigh.ToString();
                metricThird.AYearlow = getQuote.Result[0].yearLow.ToString();
                metricThird.previousClose = getQuote.Result[0].previousClose.ToString();
                metricThird.priceAvg200 = getQuote.Result[0].priceAvg200.ToString();
                metricThird.priceAvg50 = getQuote.Result[0].priceAvg50.ToString();
                var resultFinal = new FundamentalMetricData();

                resultFinal.metricFirst = metricFirst;
                resultFinal.metricSecond = metricSecond;
                resultFinal.metricThird = metricThird;
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


                ROIC.Ten = $"{roics.Take(10).Average():F2}%";
                RevenueGrowth.Ten = revenueGrowth2.ToString();
                ProfitMargin.Ten = (profitMargin2 * 100).ToString();
                FreeCashFlowMargin.Ten = $"{fcfMargins.Take(10).Average():F2}%";


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



        public async Task<ResponseDto<StockAnaResponse>> StockAnalyserResponse(StockAnalyserRequest req)
        {
            var response = new ResponseDto<StockAnaResponse>();

            try
            {
                var apiUrlIcome = _baseUrl + $"stable/income-statement?symbol={req.symbol}&period=annual&limit=1";
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


                double AverageDiluted = (double)resultIncome[0].WeightedAverageShsOutDil;
                var latestIncomeStatementRevenue = resultIncome[0].Revenue;

                double futureRevLow = (double)(latestIncomeStatementRevenue * Math.Pow(1 + (req.RevGrowth.low / 100), req.years));
                double futureRevMid = (double)(latestIncomeStatementRevenue * Math.Pow(1 + (req.RevGrowth.mid / 100), req.years));
                double futureRevHigh = (double)(latestIncomeStatementRevenue * Math.Pow(1 + (req.RevGrowth.High / 100), req.years));


                double futureNetIcomeLow = futureRevLow * (req.ProfitMargin.low / 100);
                double futureNetIcomeMid = futureRevMid * (req.ProfitMargin.mid / 100);
                double futureNetIcomeHigh = futureRevHigh * (req.ProfitMargin.High / 100);

                double futureMarketCapLow = futureNetIcomeLow * (req.PERatio.low / 100);
                double futureMarketCapMid = futureNetIcomeMid * (req.PERatio.mid / 100);
                double futureMarketCapHigh = futureNetIcomeHigh * (req.PERatio.High / 100);


                double presentValueMarketCapLow = futureMarketCapLow / Math.Pow(1 + (req.DesiredAnnReturn.low / 100), req.years);
                double presentValueMarketCapMid = futureMarketCapMid / Math.Pow(1 + (req.DesiredAnnReturn.mid / 100), req.years);
                double presentValueMarketCapHigh = futureMarketCapHigh / Math.Pow(1 + (req.DesiredAnnReturn.High / 100), req.years);


                double presentValuePricePerShareLow = presentValueMarketCapLow / AverageDiluted;
                double presentValuePricePerShareMid = presentValueMarketCapMid / AverageDiluted;
                double presentValuePricePerShareHigh = presentValueMarketCapHigh / AverageDiluted;

                var DiscountedEarningsValue = new DiscountedEarningsValue()
                {
                    Low = presentValuePricePerShareLow,
                    Mid = presentValuePricePerShareMid,
                    High = presentValuePricePerShareHigh,
                };
                var result = new StockAnaResponse();
                result.DiscountedEarningsValue = DiscountedEarningsValue;

                double futureFreeCashFlowLow = futureRevLow * (req.FreeCashFlowMargin.low / 100);
                double futureFreeCashFlowMid = futureRevMid * (req.FreeCashFlowMargin.mid / 100);
                double futureFreeCashFlowHigh = futureRevHigh * (req.FreeCashFlowMargin.High / 100);

                double futureMarketCapCashFlowLow = futureFreeCashFlowLow * (req.PFCF.low / 100);
                double futurMarketCapCashFlowMid = futureFreeCashFlowMid * (req.PFCF.mid / 100);
                double futureMarketCapCashFlowHigh = futureFreeCashFlowHigh * (req.PFCF.High / 100);

                double presentValueMarketCapCashFlowLow = futureMarketCapCashFlowLow / Math.Pow(1 + (req.DesiredAnnReturn.low / 100), req.years);
                double presentValueMarketCapCashFlowMid = futurMarketCapCashFlowMid / Math.Pow(1 + (req.DesiredAnnReturn.mid / 100), req.years);
                double presentValueMarketCapCashFlowHigh = futureMarketCapCashFlowHigh / Math.Pow(1 + (req.DesiredAnnReturn.High / 100), req.years);

                double presentValuePricePerShareCashFlowLow = presentValueMarketCapCashFlowLow / AverageDiluted;
                double presentValuePricePerShareCashFlowMid = presentValueMarketCapCashFlowMid / AverageDiluted;
                double presentValuePricePerShareCashFlowHigh = presentValueMarketCapCashFlowHigh / AverageDiluted;

                var DiscountedFreeCashFlowValue = new DiscountedFreeCashFlowValue()
                {
                    Low = presentValuePricePerShareCashFlowLow,
                    Mid = presentValuePricePerShareCashFlowMid,
                    High = presentValuePricePerShareCashFlowHigh,
                };


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

    }
}
