using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class AddContentReq
    {
        public string Content { get; set; }
        public string Category { get; set; }
    }
}
