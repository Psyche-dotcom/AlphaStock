using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AlpaStock.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
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

        [HttpPost("login")]
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("info")]
        public async Task<IActionResult> UserInfoAsync()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
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

        [HttpPost("forgot_password")]
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

        [HttpPost("reset_password")]
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

        [HttpPost("confirm-email")]
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

        // Admin-only endpoints
        [Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok();
            /* var result = await _accountService.GetAllUsers();
             return Ok(result);*/
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return Ok();
            /* var result = await _accountService.DeleteUser(id);
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
             }*/
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id)
        {
            return Ok();
            /*
            var result = await _accountService.UpdateUser(id, updateUserDto);
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
            }*/
        }
    }
}
