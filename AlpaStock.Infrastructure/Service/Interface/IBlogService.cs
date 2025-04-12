using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Core.DTOs.Response.Blog;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Util;
using Microsoft.AspNetCore.Http;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IBlogService
    {
        Task<ResponseDto<string>> DeleteBlogPost(string BlogPostId);
        Task<ResponseDto<string>> UpdateBlogPost(string BlogPostId, AddContentReq req);
        Task<ResponseDto<string>> CreateBlogReq(AddContentReq req, string userid);
        Task<ResponseDto<string>> BlogLikeAndUnlike(LikeBlogReq req, string userid);
        Task<ResponseDto<Comment>> BlogComment(AddComment req, string userid);
        Task<ResponseDto<CommentReply>> BlogCommentReply(AddCommentReplyReq req, string userid);
        Task<ResponseDto<string>> CommentLikeAndUnlike(AddCommentLikeReq req, string userid);
        Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllBlogResponseDto>>>> RetrieveAllBlog(int pageNumber, int perPageSize,
            string Category, string Status,
            string? UserId, string? sinceDate, string? Search);
        Task<ResponseDto<string>> UploadPictureToCloud(string fileName, IFormFile file);
        Task<ResponseDto<SingleBlogResponse>> RetrieveSingleBlogPost(string BlogPostId, string userId);
        Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentonBlogPost>>>> RetrieveSingleBlogPostComent(int pageNumber, int perPageSize, string BlogPostId, string userId);
        Task<ResponseDto<PaginatedGenericDto<IEnumerable<AllCommentReply>>>> RetrieveSingleBlogPostComentReply(int pageNumber, int perPageSize, string CommentId);
        Task<ResponseDto<string>> UpdateBlogStatus(string BlogPostId, BlogStatus Status);
    }
}
