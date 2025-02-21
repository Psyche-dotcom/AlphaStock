

using AlpaStock.Core.Repositories.Implementation;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Implementation;
using AlpaStock.Infrastructure.Service.Interface;

namespace AlpaStock.Api.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IAlphaRepository<>), typeof(AlphaRepository<>));
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<IEmailServices, EmailService>();
         


        }
    }
}
