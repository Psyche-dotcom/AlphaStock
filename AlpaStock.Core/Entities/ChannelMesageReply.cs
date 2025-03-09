namespace AlpaStock.Core.Entities
{
    public class ChannelMesageReply : BaseEntity
    {
        public CommunityChannelMessage ChannelMessage { get; set; }
        public string ChannelMessageId { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string SentById { get; set; }
        public ApplicationUser SentBy { get; set; }
    }
}
