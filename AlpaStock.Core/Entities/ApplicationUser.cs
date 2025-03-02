using Microsoft.AspNetCore.Identity;

namespace AlpaStock.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public bool isSuspended { get; set; } = false;
        public string? ProfilePicture { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<Payments> Payments { get; set; }
        public ConfirmEmailToken ConfirmEmailToken { get; set; }
        public ForgetPasswordToken ForgetPasswordToken { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;


    }
}
