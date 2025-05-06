using AlpaStock.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlpaStock.Core.Context
{
    public class AlphaContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }
        public DbSet<ForgetPasswordToken> ForgetPasswordTokens { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<SubscriptionFeatures> SubscriptionFeatures { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<UserSavePiller> UserSavePillers { get; set; }
        public DbSet<StockWishList> StockWishLists { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<ChannelCategory> ChannelCategories { get; set; }
        public DbSet<ChannelMesageReply> ChannelMesageReplies { get; set; }
        public DbSet<ChannelMessageLike> ChannelMessageLikies { get; set; }
        public DbSet<ChannelMessageUnLike> ChannelMessageUnLikies { get; set; }
        public DbSet<CommunityChannel> CommunityChannels { get; set; }
        public DbSet<CommunityChannelMessage> CommunityChannelMessages { get; set; }
        public DbSet<FavoriteChannelMessage> FavoriteChannelMessages { get; set; }
        public DbSet<UserCommunityChannel> UserCommunityChannels { get; set; }
        
        public AlphaContext(DbContextOptions options) : base(options) { }

    }
}
