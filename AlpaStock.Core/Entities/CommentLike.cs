namespace AlpaStock.Core.Entities
{
    public class CommentLike : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Comment Comment { get; set; }
        public string CommentId { get; set; }
    }
}
