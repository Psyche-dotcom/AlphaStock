

namespace AlpaStock.Core.DTOs.Response.Auth
{
    public class DisplayFindUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ActiveSubcriptionName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public bool IsSuspendUser { get; set; }
        public bool isSubActive { get; set; }
        public DateTime Created { get; set; }
    }
}