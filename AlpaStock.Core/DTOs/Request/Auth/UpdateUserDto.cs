
using System.ComponentModel.DataAnnotations;

namespace AlpaStock.Core.DTOs.Request.Auth
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }
}