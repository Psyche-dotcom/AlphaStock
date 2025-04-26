using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class CommunityChannelMessageRead : BaseEntity
    {
        public string MessageId { get; set; }
        public CommunityChannelMessage Message { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }

}
