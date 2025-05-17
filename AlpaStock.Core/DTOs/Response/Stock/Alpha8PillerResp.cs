using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class Alpha8PillerResp
    {
        public string amount { get; set; }
        public string header { get; set; }
        public bool isActive { get; set; }
    }
}
