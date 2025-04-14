using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class StockWishListResponseIsadded
    {
         public bool IsAdded { get; set; }
        public string WishListId { get; set; }
    }
}
