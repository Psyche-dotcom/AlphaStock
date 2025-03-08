using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class SinglePostReq
    {
        public string BlogPostId { get; set; }
        public string UserId { get; set; }
    }
}
