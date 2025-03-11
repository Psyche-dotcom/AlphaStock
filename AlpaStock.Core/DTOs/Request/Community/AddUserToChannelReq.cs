using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Community
{
    public class AddUserToChannelReq
    {
        public string ChannelId { get; set; }
        public string UserId { get; set; }
    }
}
