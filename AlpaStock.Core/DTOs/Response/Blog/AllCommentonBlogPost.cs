using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Blog
{
    public class AllCommentonBlogPost
    {
        public string CommentId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string UserImgUrl { get; set; }
        public DateTime CommentDate { get; set; }
        public bool isLiked { get; set; }
        public int LikeCount { get; set; }
    }
}
