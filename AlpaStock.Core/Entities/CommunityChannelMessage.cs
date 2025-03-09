namespace AlpaStock.Core.Entities
{
    public class CommunityChannelMessage : BaseEntity
    {
        public ICollection<ChannelMesageReply> ChannelMesageReplies { get; set; }
        public ICollection<ChannelMessageLike> ChannelMessageLikes { get; set; }
        public ICollection<ChannelMessageUnLike> ChannelMessageUnLikes { get; set; }
        public ChannelCategory ChannelCategory { get; set; }
        public string ChannelCategoryId { get; set; }
        public string Message { get; set; }
        public bool IsLiked { get; set; }
        public bool IsUnLiked { get; set; }
        public string MessageType { get; set; }
        public string SentById { get; set; }
        public ApplicationUser SentBy { get; set; }
    }
}
