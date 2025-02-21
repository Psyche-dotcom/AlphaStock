using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Entities
{
    public class ForgetPasswordToken : BaseEntity
    {
        public string token { get; set; }
        public string gentoken { get; set; }
        public ApplicationUser user { get; set; }
        public string userid { get; set; }
    }
}
