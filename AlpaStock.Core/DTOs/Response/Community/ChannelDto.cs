using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Community
{
    public class ChannelDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ChannelRoomName { get; set; }
        public int UnreadCount { get; set; }
    }
}
