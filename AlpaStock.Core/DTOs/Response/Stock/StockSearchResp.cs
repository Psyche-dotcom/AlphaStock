using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class StockSearchResp
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string ExchangeFullName { get; set; }
        public string Exchange { get; set; }
    }
}
