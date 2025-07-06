using AlpaStock.Core.DTOs.Request.Stock;
using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("add-wishlist")]
        public async Task<IActionResult> AddWishListAsync(AddToWishListReq req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var result = await _stockService.AddStockWishList(userid, req.StockSymbol, req.LowerLimit, req.UpperLimit);

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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("update-wishlist")]
        public async Task<IActionResult> UpdateWishListAsync(UpdateStockWishlistReq req)
        {

            var result = await _stockService.UpdateStockWishList(req.StockwishlistId, req.LowerLimit, req.UpperLimit);

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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete-wishlist")]
        public async Task<IActionResult> DeleteStockWishListAsync(DeleteStockWishList req)
        {

            var result = await _stockService.DeleteStockWishList(req.StockwishlistId);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-wishlist")]
        public async Task<IActionResult> GetWishListAsync()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var result = await _stockService.GetUserStockWishList(userid);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-wishlist-is-added")]
        public async Task<IActionResult> CheckWishListAsync(string symbol)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var result = await _stockService.IsAddStockWishList(userid, symbol);

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
        [HttpGet("info/profile")]
        public async Task<IActionResult> GetStockProfile(string symbol)
        {
            var result = await _stockService.GetOtherStockInfo(symbol);

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
        [HttpGet("search")]
        public async Task<IActionResult> SearchStockAsync(string symbol)
        {
            var result = await _stockService.SearchStock(symbol);

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
        [HttpGet("historical/eod")]
        public async Task<IActionResult> GetSockEod(string symbol, string startDate, string endDate)
        {
            var result = await _stockService.HistoryCalPriceEOD(symbol, startDate, endDate);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("income-statement")]
        public async Task<IActionResult> GetStockIncomeStatement(string symbol, string period, string duration)
        {
            var result = await _stockService.GetStockIncomeStatement(symbol, period,duration);

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
        [HttpGet("income-statement-ttm")]
        public async Task<IActionResult> GetStockIncomeStatement(string symbol, string period)
        {
            var result = await _stockService.GetStockIncomeStatementTTM(symbol, period);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("balance-sheet")]
        public async Task<IActionResult> GetStockBalanceSheet(string symbol, string period, string duration)
        {
            var result = await _stockService.GetStockBalanceSheet(symbol, period,duration);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("balance-sheet-ttm")]
        public async Task<IActionResult> GetStockBalanceSheetTTM(string symbol, string period)
        {
            var result = await _stockService.GetStockBalanceSheetTTM(symbol, period);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("cash-flow")]
        public async Task<IActionResult> GetStockCashFlowStatement(string symbol, string period, string duration)
        {
            var result = await _stockService.GetStockCashFlowStatement(symbol, period, duration);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("cash-flow-ttm")]
        public async Task<IActionResult> GetStockCashFlowStatementTTM(string symbol, string period)
        {
            var result = await _stockService.GetStockCashFlowStatementTTM(symbol, period);

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

       //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("fundamental-metric")]
        public async Task<IActionResult> FundamentalMetricDatas(string symbol, string period)
        {
            var result = await _stockService.Metrics(symbol, period);

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
      
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("alpha-8-piller-screener")]
        public async Task<IActionResult> AlphaStock8Pillers(string symbol, string period)
        {
            var result = await _stockService.AlphaStock8Pillers(symbol, period);

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
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("stock-analyer-stats")]
        public async Task<IActionResult> StockAnalyzerStats(string symbol, string period)
        {
            var result = await _stockService.StockAnalyserRequest(symbol, period);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("stock-alpha-stats")]
        public async Task<IActionResult> StockAlphaStats(string symbol, string period)
        {
            var result = await _stockService.StockAlphaRequest(symbol, period);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("stock-analyer-stats/predict")]
        public async Task<IActionResult> StockAnalyzerStatsPredict(StockAnalyserRequest request)
        {
            var result = await _stockService.StockAnalyserResponse(request);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("mypiller/update")]
        public async Task<IActionResult> UpdateMyPiler(List<MyAlphaReq> request)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var result = await _stockService.AddMyAlpha(userid, request);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("mypiller/current")]
        public async Task<IActionResult> RetrieveMyCurrentAlpha()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var result = await _stockService.RetrieveMyAlpha(userid);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("stock-analyer-stats/predict/hisory")]
        public async Task<IActionResult> StockAnalyzerStatsPredictHistory(StockAnalysisHisotry request)
        {
            return Ok();
            /* var result = await _stockService.StockAnalyserResponse(request);

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
             }*/
        }


        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("news/general")]
        public async Task<IActionResult> GetGeneralStockNews(string page, string limit)
        {


            var result = await _stockService.GetGeneralStockNews(page, limit);

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
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("news/press_release")]
        public async Task<IActionResult> GetStockPressNews(string symbol, string page, string limit)
        {


            var result = await _stockService.GetStockPressNews(symbol, page, limit);

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
        [HttpGet("news/general/press_release")]
        public async Task<IActionResult> GetStockGeneralPressNews(string page, string limit)
        {


            var result = await _stockService.GetStockPressGeneralNews( page, limit);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("news/specific_stock_news")]
        public async Task<IActionResult> GetStockNews(string symbol, string page, string limit)
        {


            var result = await _stockService.GetStockNews(symbol, page, limit);

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
