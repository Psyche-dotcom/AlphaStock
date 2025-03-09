using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class ChannelMessageUnLike
   : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string MessageId { get; set; }
        public CommunityChannelMessage Message { get; set; }
    }
}
