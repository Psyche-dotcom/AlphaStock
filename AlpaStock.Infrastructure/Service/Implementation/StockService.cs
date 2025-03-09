using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Infrastructure.Service.Interface;
using CloudinaryDotNet.Actions;
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

        public StockService(ILogger<StockService> logger,
            IApiClient apiClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _apiClient = apiClient;
            _configuration = configuration;
            _baseUrl = _configuration["FMP:BASEURL"];
        }

       

        public async Task<ResponseDto<IEnumerable<StockResp>>> GetStockQuote( string symbol)
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
                    response.ErrorMessages = new List<string>() { "Stock list is empty" };
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
    }
}
