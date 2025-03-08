using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Blog
{
    public class AllBlogResponseDto
    {
        public string Id { get; set; }
        public string PublisherName { get; set; }
        public string PublisherUsername { get; set; }
        public string Title { get; set; }
        public string BlogThumbnailUrl { get; set; }
        public int LikeCount { get; set; }
        public string Status  { get; set; }
        public string Category  { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublisherImgUrl { get; set; }
    }
}
