using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Request.Notification;
using AlpaStock.Core.DTOs.Response.Auth;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AlpaStock.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailServices _emailServices;

        public UserController(IAccountService accountService, IEmailServices emailServices)
        {
            _accountService = accountService;
            _emailServices = emailServices;
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> Register(SignUp req)
        {
            var result = await _accountService.RegisterUser(req, "User");
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
        [HttpPost("user/social/login")]
        public async Task<IActionResult> SocialLogin(SocialLoginDto req)
        {
            var result = await _accountService.SignInRegisterSocialAccount(req.Token,req.GenericPassword);
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

        [HttpPost("user/login")]
        public async Task<IActionResult> Login(SignInModel req)
        {
            var result = await _accountService.LoginUser(req);
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
        [HttpGet("user/info")]
        public async Task<IActionResult> UserInfoAsync()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var result = await _accountService.UserInfoAsync(userid);
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


        [HttpPost("user/forgot_password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _accountService.ForgotPassword(email);
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
        [HttpPatch("user/update_details/{email}")]
        public async Task<IActionResult> UpdateUserInfo(string email, UpdateUserDto updateUser)
        {
            var result = await _accountService.UpdateUser(email, updateUser);
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
        [HttpPost("admin/suspend_user/{email}")]
        public async Task<IActionResult> SuspendUser(string email)
        {
            var result = await _accountService.SuspendUserAsync(email);
            if (result.StatusCode == 200)
            {
                var message = new Message(new string[] { email }, "Suspend", $"<p>You have been suspended on alpha stock, please contact admin<p>");
                _emailServices.SendEmail(message);
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
        [HttpPost("admin/unsuspend_user/{email}")]
        public async Task<IActionResult> UnSuspendUser(string email)
        {
            var result = await _accountService.UnSuspendUserAsync(email);
            if (result.StatusCode == 200)
            {
                var message = new Message(new string[] { email }, "Unsuspend", $"<p>Congrat, you have been unsuspended on alpha stock, you can continue to use our service<p>");
                _emailServices.SendEmail(message);
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
        [HttpPatch("user/update_picture/{email}")]
        public async Task<IActionResult> UploadUserPicture(string email, IFormFile file)
        {
            var result = await _accountService.UploadUserProfilePicture(email, file);
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
        [HttpDelete("user/delete_user/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await _accountService.DeleteUser(email);
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
        [HttpPost("user/reset_password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var result = await _accountService.ResetPassword(resetPassword);
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

        [HttpPost("user/confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailTokenDto token)
        {
            var result = await _accountService.ConfirmEmailAsync(token.token, token.email);
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
        [HttpGet("admin/userstats")]
        public async Task<IActionResult> GetAllUserStatsAsync()
        {
            var result = await _accountService.GetUserStatisticsChartAsync();
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
        [HttpGet("all/{per_page_size}/{page_number}")]
        public async Task<IActionResult> GetAllUserAsync(int page_number, int per_page_size, string? sinceDate, string? name, UserFilter filter)
        {
            var result = await _accountService.GetAllUsersAsync(page_number, per_page_size,sinceDate, name, filter);
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
