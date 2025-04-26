namespace AlpaStock.Core.Entities
{
    public class CommunityChannelMessage : BaseEntity
    {
        public ICollection<ChannelMesageReply> ChannelMesageReplies { get; set; }
        public ICollection<ChannelMessageLike> ChannelMessageLikes { get; set; }
        public ICollection<ChannelMessageUnLike> ChannelMessageUnLikes { get; set; }
        public ICollection<CommunityChannelMessageRead> MessageReads { get; set; }
        public ChannelCategory ChannelCategory { get; set; }
        public string ChannelCategoryId { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string SentById { get; set; }
        public ApplicationUser SentBy { get; set; }
    }
}
