using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Blog
{
    public class AllCommentReply
    {
        public string ReplyId { get; set; }
        public string Name { get; set; }
        public string ReplyContent { get; set; }
        public string UserImgUrl { get; set; }
        public DateTime ReplyDate { get; set; }
    }
}
