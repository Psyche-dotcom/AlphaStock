﻿namespace AlpaStock.Core.Entities
{
    public class ChannelMessageLike : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string MessageId { get; set; }
        public CommunityChannelMessage Message { get; set; }
    }
}
