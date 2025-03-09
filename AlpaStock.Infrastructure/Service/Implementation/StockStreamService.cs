using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class StockStreamService : IStockStreamService
    {
        private readonly ILogger<StockStreamService> _logger;
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public StockStreamService(ILogger<StockStreamService> logger,
            IApiClient apiClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _apiClient = apiClient;
            _configuration = configuration;
            _baseUrl = _configuration["FMP:BASEURL"];
        }
        public async IAsyncEnumerable<string> GetStockMarketLeaderAsync(string leaderType)
        {
            var interval = _configuration.GetValue<int>("StockIntervalSeconds");
            var apiUrl = string.Empty;

            switch (leaderType)
            {
                case "MostTraded":
                    apiUrl = $"{_baseUrl}stable/most-actives";
                    break;
                case "MostGainer":
                    apiUrl = $"{_baseUrl}stable/biggest-gainers";
                    break;
                case "MostLoser":
                    apiUrl = $"{_baseUrl}stable/biggest-losers";
                    break;
                default:
                    throw new ArgumentException("Invalid leader type", nameof(leaderType));
            }

            while (true)
            {
                var result = await FetchStockLeaderDataAsync(apiUrl);
                yield return result;
                await Task.Delay(TimeSpan.FromSeconds(interval));
            }
        }


        private async Task<string> FetchStockLeaderDataAsync(string apiUrl)
        {
            try
            {
                var result = await _apiClient.GetAsync<string>(apiUrl);
                return result.Content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stock market leader data");
                return string.Empty;
            }
        }
    }
}
