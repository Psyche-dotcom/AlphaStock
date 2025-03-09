using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Infrastructure.Service.Implementation;
using AlpaStock.Infrastructure.Service.Interface;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;

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
        
        [HttpGet("quote")]
        public async Task<IActionResult> GetStockQuote(string symbol)
        {
            var result = await _stockService.GetStockQuote(symbol);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
    }

}
