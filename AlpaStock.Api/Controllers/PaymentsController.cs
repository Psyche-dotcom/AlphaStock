using AlpaStock.Core.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Dating.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _pay;
     
        public PaymentsController(IPaymentService pay)
        {
            _pay = pay;
          
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create/buy_plan")]
        public async Task<IActionResult> GetOrder(string plainid)
        {
             var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _pay.MakeOrder(userid, plainid);

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
        [HttpGet("webhook/confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(string token)
        {
            var result = await _pay.ConfirmPayment(token);
            if (result.StatusCode == 200)
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
        /*  [Authorize(AuthenticationSchemes = "Bearer", Roles ="ADMIN")]
          [HttpGet("get-payment-by-orderId")]
          public async Task<IActionResult> GetPaymentByOrderId(string OrderId)
          {
              var data = await _paydb.GetPaymentById(OrderId);
              return Ok(data);
          }*/
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user/all/{user_id}/{perPageSize}/{pageNumber}")]
        public async Task<IActionResult> UserPaymentHistory(string user_id, int pageNumber, int perPageSize)
        {
            var result = await _pay.RetrieveUserAllPaymentAsync(user_id, pageNumber, perPageSize);
            if (result.StatusCode == 200)
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
        [HttpGet("user/all/{perPageSize}/{pageNumber}")]
        public async Task<IActionResult> AllPaymentHistory(int pageNumber, int perPageSize)
        {
            var result = await _pay.RetrieveAllPaymentAsync(pageNumber, perPageSize);
            if (result.StatusCode == 200)
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