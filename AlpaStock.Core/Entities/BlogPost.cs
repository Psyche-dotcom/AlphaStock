using AlpaStock.Core.Util;

namespace AlpaStock.Core.Entities
{
    public class BlogPost : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string BlogThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Status { get; set; } = "Pending";
        public ICollection<BlogPostLike> BlogPostLikes { get; set; }
        public ICollection<Comment> Comments { get; set; }


    }
}
