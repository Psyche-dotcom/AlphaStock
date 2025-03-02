namespace AlpaStock.Core.Entities
{
    public class CommentReply : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public Comment Comment { get; set; }
        public string CommentId { get; set; }
    }
}
