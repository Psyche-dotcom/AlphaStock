using Microsoft.AspNetCore.Identity;

namespace AlpaStock.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public bool isSuspended { get; set; } = false;
        public string? ProfilePicture { get; set; }
      
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<Payments> Payments { get; set; }
        public ICollection<StockWishList> StockWishList { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<BlogPostLike> BlogPostLikes{ get; set; }
        public ICollection<CommentLike> CommentLikes{ get; set; }
        public ICollection<CommentReply> CommentReplies{ get; set; }
       
        public ICollection<ChannelCategory> ChannelCategory { get; set; }
        public ICollection<ChannelMesageReply> ChannelMesageReply { get; set; }
        public ICollection<ChannelMessageLike> ChannelMessageLike { get; set; }
        public ICollection<ChannelMessageUnLike> ChannelMessageUnLike { get; set; }
        public ICollection<CommunityChannel> CommunityChannel { get; set; }
        public ICollection<CommunityChannelMessage> CommunityChannelMessage { get; set; }
        public ICollection<UserCommunityChannel> UserCommunityChannel { get; set; }
      
     
        public ConfirmEmailToken ConfirmEmailToken { get; set; }
        public ForgetPasswordToken ForgetPasswordToken { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;


    }
}
