using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Stock
{
    public class FreeCashFlowMarginReq
    {
        public double low { get; set; }
        public double? mid { get; set; }
        public double? High { get; set; }
    }
}
