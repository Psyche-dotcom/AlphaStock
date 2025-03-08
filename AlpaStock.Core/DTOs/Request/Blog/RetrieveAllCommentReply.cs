using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class RetrieveAllCommentReply
    {
        public int pageNumber { get; set; }
        public string CommentId { get; set; }
        public int perPageSize { get; set; }
    }
}
