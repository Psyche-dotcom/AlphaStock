using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Core.DTOs.Response.Blog;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Core.Util;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class BlogService : IBlogService
    {

        private readonly IAccountRepo _accountRepo;
        private readonly IAlphaRepository<BlogPost> _blogPostRepo;
        private readonly ILogger<BlogService> _logger;
        private readonly IAlphaRepository<BlogPostLike> _blogPostLikeRepo;
        private readonly IAlphaRepository<Comment> _commentRepo;
        private readonly IAlphaRepository<CommentLike> _commentLikeRepo;
        private readonly IAlphaRepository<CommentReply> _commentreplyRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public BlogService(IAlphaRepository<CommentReply> commentreplyRepo,
            IAlphaRepository<CommentLike> commentLikeRepo,
            IAlphaRepository<Comment> commentRepo,
            IAlphaRepository<BlogPostLike> blogPostLikeRepo,
            ILogger<BlogService> logger,
            IAlphaRepository<BlogPost> blogPostRepo,
            ICloudinaryService cloudinaryService,
            IAccountRepo accountRepo)
        {
            _commentreplyRepo = commentreplyRepo;
            _commentLikeRepo = commentLikeRepo;
            _commentRepo = commentRepo;
            _blogPostLikeRepo = blogPostLikeRepo;
            _logger = logger;
            _blogPostRepo = blogPostRepo;
            _cloudinaryService = cloudinaryService;
            _accountRepo = accountRepo;
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
        public async Task<ResponseDto<string>> CreateBlogReq(AddContentReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var AddSubPlan = await _blogPostRepo.Add(new()
                {
                    BlogThumbnailUrl = req.BlogThumbnailUrl,
                    Title = req.Title,
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
        public async Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllBlogResponseDto>>>> RetrieveAllBlog(
            int pageNumber, int perPageSize, string Category, string Status, string? UserId, string? sinceDate, string? Search)
        {
            var response = new ResponseDto<PaginatedGenericDto<IEnumerable<AllBlogResponseDto>>>();
            try
            {
                pageNumber = Math.Max(pageNumber, 1);
                perPageSize = Math.Max(perPageSize, 4);

                var retrieveBlog = _blogPostRepo.GetQueryable();


                if (!string.IsNullOrEmpty(UserId))
                {
                    retrieveBlog = retrieveBlog.Where(u => u.UserId == UserId);
                }

                if (!string.IsNullOrEmpty(sinceDate) && DateTime.TryParse(sinceDate, out DateTime parsedDate))
                {
                    DateTime utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                    retrieveBlog = retrieveBlog.Where(u => u.Created >= utcDate);
                }

                if (!string.IsNullOrEmpty(Search))
                {
                    retrieveBlog = retrieveBlog.Where(u => u.Title.ToLower().Contains(Search.ToLower()));
                }

                var allBlog = retrieveBlog.OrderBy(u => u.Created).Select(p => new AllBlogResponseDto
                {
                    Id = p.Id,
                    LikeCount = p.BlogPostLikes.Count(),
                    PublishedDate = p.Created,
                    PublisherImgUrl = p.User.ProfilePicture,
                    PublisherName = $"{p.User.FirstName} {p.User.LastName}",
                    PublisherUsername = p.User.UserName,
                    Status = p.Status,
                    Title = p.Title,
                    Category = p.Category,
                    BlogThumbnailUrl = p.BlogThumbnailUrl
                });

                if (Status != "All")
                {
                    allBlog = allBlog.Where(u => u.Status == Status);
                }

                if (Category != "All")
                {
                    allBlog = allBlog.Where(p => p.Category == Category);
                }

                var paginatedBlog = await allBlog
                    .Skip((pageNumber - 1) * perPageSize)
                    .Take(perPageSize)
                    .ToListAsync();

                var totalCount = await allBlog.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);

                var result = new PaginatedGenericDto<IEnumerable<AllBlogResponseDto>>
                {
                    CurrentPage = pageNumber,
                    PageSize = perPageSize,
                    TotalPages = totalPages,
                    Result = paginatedBlog,
                };

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog posts");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot retrieve blog posts at the moment" };
                response.StatusCode = 500; // Use 500 for server errors
                return response;
            }
        }
        public async Task<ResponseDto<SingleBlogResponse>> RetrieveSingleBlogPost(string BlogPostId, string userId)
        {
            var response = new ResponseDto<SingleBlogResponse>();
            try
            {
                var result = await _blogPostRepo.GetQueryable()
                    .Where(p => p.Id == BlogPostId)
                    .Select(p => new SingleBlogResponse
                    {
                        Id = p.Id,
                        LikeCount = p.BlogPostLikes.Count(),
                        PublishedDate = p.Created,
                        PublisherImgUrl = p.User.ProfilePicture,
                        PublisherName = $"{p.User.FirstName} {p.User.LastName}",
                        PublisherUsername = p.User.UserName,
                        Status = p.Status,
                        Title = p.Title,
                        Category = p.Category,
                        BlogContent = p.Content,
                        BlogThumbnailUrl = p.BlogThumbnailUrl,
                        isLiked = p.BlogPostLikes.Any(like => like.UserId == userId)
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Not Found";
                    response.ErrorMessages = new List<string> { "Blog post not found" };
                    return response;
                }

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog post");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot retrieve blog post at the moment" };
                response.StatusCode = 500;
                return response;
            }
        } 
        public async Task<ResponseDto<string>> DeleteBlogPost(string BlogPostId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var result = await _blogPostRepo.GetQueryable()
                    .FirstOrDefaultAsync(p => p.Id == BlogPostId);

               
                if (result == null)
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Not Found";
                    response.ErrorMessages = new List<string> { "Blog post not found" };
                    return response;
                }
                _blogPostRepo.Delete(result);
                await _blogPostRepo.SaveChanges();

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Blog Post Delete success fully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Delete blog post");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot Delete blog post at the moment" };
                response.StatusCode = 500;
                return response;
            }
        }

        public async Task<ResponseDto<string>> UpdateBlogPost(string BlogPostId, AddContentReq req)
        {
            var response = new ResponseDto<string>();
            try
            {
                var result = await _blogPostRepo.GetQueryable()
                    .FirstOrDefaultAsync(p => p.Id == BlogPostId);


                if (result == null)
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Not Found";
                    response.ErrorMessages = new List<string> { "Blog post not found" };
                    return response;
                }
                result.Content = req.Content;
                result.BlogThumbnailUrl = req.BlogThumbnailUrl;
                result.Category= req.Category;
                result.Title = req.Title;
                   
                _blogPostRepo.Update(result);
                await _blogPostRepo.SaveChanges();

                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Blog Post Updated success fully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updated blog post");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot Updated blog post at the moment" };
                response.StatusCode = 500;
                return response;
            }
        }


        public async Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentonBlogPost>>>> RetrieveSingleBlogPostComent(int pageNumber, int perPageSize, string BlogPostId, string userId)
        {
            pageNumber = Math.Max(pageNumber, 1);
            perPageSize = Math.Max(perPageSize, 5);
            var response = new ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentonBlogPost>>>();
            try
            {
                var result = _commentRepo.GetQueryable()
                    .Where(p => p.BlogPostId == BlogPostId)
                    .Select(p => new AllCommentonBlogPost
                    {
                        CommentId = p.Id,
                        LikeCount = p.CommentLike.Count(),
                        CommentDate = p.Created,
                        UserImgUrl = p.User.ProfilePicture,
                        Name = $"{p.User.FirstName} {p.User.LastName}",
                        Comment = p.Content,
                        isLiked = p.CommentLike.Any(like => like.UserId == userId)
                    });



                var paginatedBlog = await result
                   .Skip((pageNumber - 1) * perPageSize)
                   .Take(perPageSize)
                   .ToListAsync();

                var totalCount = await result.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);

                var CommentResult = new PaginatedGenericDto<IEnumerable<AllCommentonBlogPost>>
                {
                    CurrentPage = pageNumber,
                    PageSize = perPageSize,
                    TotalPages = totalPages,
                    Result = paginatedBlog,
                };
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = CommentResult;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog post");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot retrieve blog post comment at the moment" };
                response.StatusCode = 500;
                return response;
            }
        }
        public async Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentReply>>>> RetrieveSingleBlogPostComentReply(int pageNumber, int perPageSize, string CommentId)
        {
            pageNumber = Math.Max(pageNumber, 1);
            perPageSize = Math.Max(perPageSize, 5);
            var response = new ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentReply>>>();
            try
            {
                var result = _commentreplyRepo.GetQueryable()
                    .Where(p => p.CommentId == CommentId)
                    .Select(p => new AllCommentReply
                    {
                        ReplyId = p.Id,

                        ReplyDate = p.Created,
                        UserImgUrl = p.User.ProfilePicture,
                        Name = $"{p.User.FirstName} {p.User.LastName}",
                        ReplyContent = p.Content,

                    });



                var paginatedBlog = await result
                   .Skip((pageNumber - 1) * perPageSize)
                   .Take(perPageSize)
                   .ToListAsync();

                var totalCount = await result.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);

                var CommentReplyResult = new PaginatedGenericDto<IEnumerable<AllCommentReply>>
                {
                    CurrentPage = pageNumber,
                    PageSize = perPageSize,
                    TotalPages = totalPages,
                    Result = paginatedBlog,
                };
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = CommentReplyResult;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog post");
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Cannot retrieve blog post comment reply at the moment" };
                response.StatusCode = 500;
                return response;
            }
        }
        public async Task<ResponseDto<string>> UpdateBlogStatus(string BlogPostId, BlogStatus Status)
        {
            var response = new ResponseDto<string>();
            try
            {
                var AddSubPlan = await _blogPostRepo.GetByIdAsync(BlogPostId);
                if (AddSubPlan == null)
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string> { "Blog post not found" };
                    return response;
                }
                AddSubPlan.Status = Status.ToString();
                AddSubPlan.DateUpdated = DateTime.UtcNow;
                _blogPostRepo.Update(AddSubPlan);
                await _blogPostRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = $"Blog post status update to {AddSubPlan.Status} successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Blog status not updated successfully" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<string>> UploadPictureToCloud(string fileName, IFormFile file)
        {
            var response = new ResponseDto<string>();
            try
            {
                var UniqueFile = fileName + _accountRepo.GenerateToken();
                var uploadImage = await _cloudinaryService.UploadPhoto(file, fileName);
                if (uploadImage == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in uploading profile picture for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = uploadImage.Url.ToString();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in uploading picture" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }


    }
}
