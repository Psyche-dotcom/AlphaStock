using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Blog;
using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IBlogService
    {
        Task<ResponseDto<string>> CreateBlogReq(AddContentReq req, string userid);
        Task<ResponseDto<string>> BlogLikeAndUnlike(LikeBlogReq req, string userid);
        Task<ResponseDto<Comment>> BlogComment(AddComment req, string userid);
        Task<ResponseDto<CommentReply>> BlogCommentReply(AddCommentReplyReq req, string userid);
        Task<ResponseDto<string>> CommentLikeAndUnlike(AddCommentLikeReq req, string userid);
    }
}
