using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AlpaStock.Api.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBlogPost(AddContentReq req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.CreateBlogReq(req,userid);

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
        [HttpPost("like_unlike")]
        public async Task<IActionResult> LikeAndUnlikePost(LikeBlogReq req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.BlogLikeAndUnlike(req,userid);

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
        [HttpPost("comment/reply/add")]
        public async Task<IActionResult> AddCommentReply(AddCommentReplyReq req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.BlogCommentReply(req, userid);

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
        [HttpPost("comment/like_unlike")]
        public async Task<IActionResult> AddCommentLikeAndUnlike(AddCommentLikeReq req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.CommentLikeAndUnlike(req, userid);

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
