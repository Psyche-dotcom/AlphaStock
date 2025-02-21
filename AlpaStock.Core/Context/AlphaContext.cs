using AlpaStock.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlpaStock.Core.Context
{
    public class AlphaContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }
        public DbSet<ForgetPasswordToken> ForgetPasswordTokens { get; set; } 
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public AlphaContext(DbContextOptions options) : base(options) { }

    }
}
