using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Auth
{
    public enum UserFilter
    {
        ALL,
        ACTIVE,
        UNVERIFIED,
        SUSPENDED
       
    }
}
