namespace AlpaStock.Core.Entities
{
    public class CommunityChannel : BaseEntity
    {
        public string Name { get; set; }
        public string ChannelRoomId { get; set; }
        public string CategoryId { get; set; }
        public ChannelCategory Category { get; set; }
        public string CreatedByUserId { get; set; }
        public ICollection<CommunityChannelMessage> CommunityChannelMessages { get; set; }
        public ICollection<UserCommunityChannel> UsersCommunityChannels { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
