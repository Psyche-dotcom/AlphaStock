using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Auth
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public bool isSuspended { get; set; }
        public string SubsciptionType { get; set; }
    }
}
