


using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
