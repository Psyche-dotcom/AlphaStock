using AlpaStock.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Blog
{
    public class UpdateBlogStatusReq
    {
       public string BlogPostId { get; set; }
       public BlogStatus Status { get; set; }
    }
}
