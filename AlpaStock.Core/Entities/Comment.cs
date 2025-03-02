namespace AlpaStock.Core.Entities
{
    public class Comment : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public BlogPost BlogPost { get; set; }
        public string BlogPostId { get; set; }
        public ICollection<CommentLike> CommentLike { get; set; }
        public ICollection<CommentReply> CommentReplies { get; set; }
    }
}
