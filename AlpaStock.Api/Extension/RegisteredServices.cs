

using AlpaStock.Api.MapingProfile;
using AlpaStock.Core.DTOs.Response.Payment;
using AlpaStock.Core.Repositories.Implementation;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Implementation;
using AlpaStock.Infrastructure.Service.Interface;
using PayPalCheckoutSdk.Core;

namespace AlpaStock.Api.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var paypalConfiguration = configuration.GetSection("PayPal").Get<Paypal>();
            var environment = new SandboxEnvironment(paypalConfiguration.ClientId, paypalConfiguration.Secret);
            services.AddSingleton(new PayPalHttpClient(environment));
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IAlphaRepository<>), typeof(AlphaRepository<>));
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<IEmailServices, EmailService>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddAutoMapper(typeof(ProjectProfile));
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IApiClient, ApiClient>();
            services.AddScoped<IStockStreamService, StockStreamService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStripePaymentService, StripePaymentService>();
            services.AddHttpClient();
        }
    }
}
