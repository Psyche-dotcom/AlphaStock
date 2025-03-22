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



        public async Task<ResponseDto<FundamentalMetricData>> Metrics(string symbol, string period)
        {
            var response = new ResponseDto<FundamentalMetricData>();
            var metricFirst = new MetricFirst();
            var metricSecond = new MetricSecond();
            var metricThird = new MetricThird();
            try
            {
                var getQuote = await GetStockQuote(symbol);
                if(getQuote.StatusCode != 200)
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

                metricFirst.RevenuePerShare = resultRatio[0].RevenuePerShare; 
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
                metricThird.name = getQuote.Result[0].name.ToString();
                metricThird.Symbol = getQuote.Result[0].symbol.ToString();
                metricThird.volume = getQuote.Result[0].volume.ToString();
                metricThird.previousClose  = getQuote.Result[0].previousClose.ToString();
                metricThird.priceAvg200 = getQuote.Result[0].priceAvg200.ToString();
                metricThird.change  = getQuote.Result[0].change.ToString();
                metricThird.MarketCap = getQuote.Result[0].marketCap.ToString();
                metricThird.priceAvg50 = getQuote.Result[0].priceAvg50.ToString();
                metricThird.exchange = getQuote.Result[0].exchange.ToString();




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

    }
}
