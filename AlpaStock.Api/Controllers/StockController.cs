using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Infrastructure.Service.Interface;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlpaStock.Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet("stock_list/all")]
        public async Task<IActionResult> GetAllStockList()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://financialmodelingprep.com/stable/stock-list?apikey=ZFMP0r5vu73PTyBkn56irPTH80ujvR7u"))
                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                    var response = await httpClient.SendAsync(request);
                    var result = JsonConvert.DeserializeObject<IEnumerable<AllStockListResponse>>(await response.Content.ReadAsStringAsync());
                    return Ok(result);
                }
            }
            

           

           
        }
    }

}
