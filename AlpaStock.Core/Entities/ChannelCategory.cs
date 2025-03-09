namespace AlpaStock.Core.Entities
{
    public class ChannelCategory : BaseEntity
    {
        public string Name { get; set; }
        public string CreatedByUserId { get; set; }
        public ICollection<CommunityChannel> CommunityChannels { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
