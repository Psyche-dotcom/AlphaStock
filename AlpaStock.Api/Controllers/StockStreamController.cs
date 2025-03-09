/*using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AlpaStock.Api.Controllers
{
    [Route("api/stock/stream")]
    [ApiController]
    public class StockStreamController : ControllerBase
    {
        private readonly IStockStreamService _stockService;
        private readonly ILogger<StockStreamController> _logger;
        public StockStreamController(IStockStreamService stockService,
            ILogger<StockStreamController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet("market_performance")]
        public async Task GetStockByLeaderType(string leaderType)
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            try
            {
                await foreach (var stock in _stockService.GetStockMarketLeaderAsync(leaderType))
                {
                    await Response.WriteAsync($"data: {stock}\n\n");
                    await Response.Body.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in SSE stream.");
            }
        }
       

    }
}
*/