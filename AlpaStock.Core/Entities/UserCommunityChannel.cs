namespace AlpaStock.Core.Entities
{
    public class UserCommunityChannel :BaseEntity
    {
        public CommunityChannel CommunityChannel { get; set; }
        public string CommunityChannelId { get; set; }
        public bool IsSuspended { get; set; } = false;
        public string? SuspendedBy { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
