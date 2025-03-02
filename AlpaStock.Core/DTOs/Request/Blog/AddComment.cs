using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class AddComment
    {
        public string Content { get; set; }
        public string BlogPostId { get; set; }
    }
}
