using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Subscription
{
    public class AddSubPlanReq
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
       
        public bool IsDIscounted { get; set; } = false;
        public int DiscountRate { get; set; } = 0;
        [Required]
        public string BillingInterval { get; set; }
    }
}
