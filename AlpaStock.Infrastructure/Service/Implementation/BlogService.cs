using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IAlphaRepository<BlogPost> _blogPostRepo;
        private readonly ILogger<BlogService> _logger;
        private readonly IAlphaRepository<BlogPostLike> _blogPostLikeRepo;
        private readonly IAlphaRepository<Comment> _commentRepo;
        private readonly IAlphaRepository<CommentLike> _commentLikeRepo;
        private readonly IAlphaRepository<CommentReply> _commentreplyRepo;

        public BlogService(IAlphaRepository<CommentReply> commentreplyRepo,
            IAlphaRepository<CommentLike> commentLikeRepo,
            IAlphaRepository<Comment> commentRepo,
            IAlphaRepository<BlogPostLike> blogPostLikeRepo,
            ILogger<BlogService> logger,
            IAlphaRepository<BlogPost> blogPostRepo)
        {
            _commentreplyRepo = commentreplyRepo;
            _commentLikeRepo = commentLikeRepo;
            _commentRepo = commentRepo;
            _blogPostLikeRepo = blogPostLikeRepo;
            _logger = logger;
            _blogPostRepo = blogPostRepo;
        }

        public async Task<ResponseDto<string>> CreateBlogReq(AddContentReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var AddSubPlan = await _blogPostRepo.Add(new BlogPost()
                {
                    Content = req.Content,
                    UserId = userid,
                    Category = req.Category,

                });

                await _blogPostRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Blog Created Successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Blog not created successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> BlogLikeAndUnlike(LikeBlogReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _blogPostLikeRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid && u.BlogPostId == req.BlogPostId);
                if (check != null)
                {
                    _blogPostLikeRepo.Delete(check);
                    await _blogPostLikeRepo.SaveChanges();
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = "Blog post unlike successfully";
                    return response;

                }
                var result = await _blogPostLikeRepo.Add(new BlogPostLike()
                {

                    UserId = userid,
                    BlogPostId = req.BlogPostId,

                });

                await _blogPostLikeRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Blog post like successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Blog post like and unlike service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<Comment>> BlogComment(AddComment req, string userid)
        {
            var response = new ResponseDto<Comment>();
            try
            {
                var result = await _commentRepo.Add(new Comment()
                {
                    Content = req.Content,
                    UserId = userid,
                    BlogPostId = req.BlogPostId,

                });

                await _blogPostRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Comment not added successfully" };
                response.StatusCode = 400;
                return response;
            }
        } 
        public async Task<ResponseDto<CommentReply>> BlogCommentReply(AddCommentReplyReq req, string userid)
        {
            var response = new ResponseDto<CommentReply>();
            try
            {
                var result = await _commentreplyRepo.Add(new CommentReply()
                {
                    Content = req.Content,
                    UserId = userid,
                    CommentId = req.CommentId,

                });

                await _blogPostRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Comment reply not added successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> CommentLikeAndUnlike(AddCommentLikeReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _commentLikeRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid && u.CommentId == req.CommentId);
                if (check != null)
                {
                    _commentLikeRepo.Delete(check);
                    await _commentLikeRepo.SaveChanges();
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = "Comment post unlike successfully";
                    return response;

                }
                var result = await _commentLikeRepo.Add(new CommentLike()
                {

                    UserId = userid,
                    CommentId = req.CommentId,

                });

                await _commentLikeRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Comment post like successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Comment post like and unlike service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
    }
}
