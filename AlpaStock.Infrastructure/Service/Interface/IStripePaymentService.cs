using AlpaStock.Core.DTOs;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStripePaymentService
    {
        Task<ResponseDto<string>> IntializeStripepayment(string planid, string userid);
        Task<ResponseDto<string>> confirmStripepayment(string session_id);
    }
}
