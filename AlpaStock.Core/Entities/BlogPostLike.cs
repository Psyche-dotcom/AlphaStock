using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class BlogPostLike : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    
        public BlogPost BlogPost { get; set; }
        public string BlogPostId { get; set; }
    }
}
