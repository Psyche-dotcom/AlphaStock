using AlpaStock.Core.Context;
using AlpaStock.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AlpaStock.Api.Extension
{
    public static class DbRegisteredExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AlphaContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<AlphaContext>(dbContextOptions =>
            {
                var connectionString = configuration.GetConnectionString("ProdDB");
                var maxRetryCount = 3;
                var maxRetryDelay = TimeSpan.FromSeconds(10);

                dbContextOptions.UseNpgsql(connectionString, options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null);

                });
            });
        }
    }
}
