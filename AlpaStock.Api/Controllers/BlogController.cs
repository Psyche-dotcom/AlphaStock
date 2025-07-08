using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Core.DTOs.Response.Blog;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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


            var result = await _blogService.CreateBlogReq(req, userid);

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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> updateBlogPost(AddContentReq req, string id)
        {
            var result = await _blogService.UpdateBlogPost(id, req);

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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlogPost(string id)
        {
            var result = await _blogService.DeleteBlogPost(id);

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


            var result = await _blogService.BlogLikeAndUnlike(req, userid);

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
        public async Task<IActionResult> AddCommentReply(AddCommentReplyReq REQUEST)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.BlogCommentReply(REQUEST, userid);

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
        [HttpPost("comment/add")]
        public async Task<IActionResult> AddComment(AddComment req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;


            var result = await _blogService.BlogComment(req, userid);

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
        [HttpPut("admin/update/blogstatus")]
        public async Task<IActionResult> UpdateBlogStatus(UpdateBlogStatusReq req)
        {

            var result = await _blogService.UpdateBlogStatus(req.BlogPostId, req.Status);

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
        [HttpPost("upload/picture")]
        public async Task<IActionResult> UploadPictures(UploadPictureReq req)
        {

            var result = await _blogService.UploadPictureToCloud(req.fileName, req.file);

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

        [AllowAnonymous]
        [HttpPost("post/retrieve/all")]
        public async Task<IActionResult> RetrieveAllBlog(RetrieveAllBlogFilter req)
        {
            var result = await _blogService.RetrieveAllBlog(req.pageNumber, req.perPageSize,
                req.Category, req.Status, req.UserId, req.sinceDate, req.Search);

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
        [AllowAnonymous]
        [HttpPost("post/retrieve/single")]
        public async Task<IActionResult> RetrieveSingleBlogPost(SinglePostReq req)
        {
            var result = await _blogService.RetrieveSingleBlogPost(req.BlogPostId, req.UserId);

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
        [AllowAnonymous]
        [HttpPost("comment/retrieve/all")]
        public async Task<IActionResult> RetrieveAllPostComment(BlogPostCommentReq req)
        {
            var result = await _blogService.RetrieveSingleBlogPostComent(req.pageNumber, req.perPageSize, req.BlogPostId, req.UserId);

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
        [AllowAnonymous]
        [HttpPost("comment/reply/retrieve/all")]
        public async Task<IActionResult> RetrieveSingleBlogPostComentReplyAsync(RetrieveAllCommentReply req)
        {
            var result = await _blogService.RetrieveSingleBlogPostComentReply(req.pageNumber, req.perPageSize, req.CommentId);

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
