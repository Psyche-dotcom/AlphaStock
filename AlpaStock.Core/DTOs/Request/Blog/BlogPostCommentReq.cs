namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class BlogPostCommentReq
    {
        public string UserId { get; set; }
        public int pageNumber { get; set; }
        public string BlogPostId { get; set; }
        public int perPageSize { get; set; }


    }
}
