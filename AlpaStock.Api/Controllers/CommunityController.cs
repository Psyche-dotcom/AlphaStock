using AlpaStock.Core.DTOs.Request.Community;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AlpaStock.Api.Controllers
{
    [Route("api/community")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create/channel")]
        public async Task<IActionResult> CreateChannel(AddChannel req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _communityService.AddChannel(req.CatId, userid, req.Name);

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
        [HttpPost("user/add/channel")]
        public async Task<IActionResult> AddUserToChannel(AddUserToChannelReq req)
        {

            var result = await _communityService.AddUserToChannel(req.UserId, req.ChannelId);

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
        [HttpGet("retrieve/user/channel")]
        public async Task<IActionResult> RetrieveChannel(string? UserId)
        {

            var result = await _communityService.RetrieveChannel(UserId);

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
        [HttpPost("create/category")]
        public async Task<IActionResult> CreateCommunityCategory(string CategoryName)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _communityService.CreateCommunityCategory(CategoryName,userid);

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
        [HttpGet("retrieve/category/all")]
        public async Task<IActionResult> RetrieveCommunityCategory()
        {

            var result = await _communityService.RetrieveCommunityCategory();

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
