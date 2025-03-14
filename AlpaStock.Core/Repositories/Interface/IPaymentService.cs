using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Response.Payment;

namespace AlpaStock.Core.Repositories.Interface
{
    public interface IPaymentService
    {
        Task<ResponseDto<string>> MakeOrder(string paymentType, string userid);
        Task<ResponseDto<string>> ConfirmPayment(string token);
        Task<ResponseDto<PaginatedPaymentInfo>> RetrieveUserAllPaymentAsync(string userid, int pageNumber, int perPageSize);
        Task<ResponseDto<PaginatedPaymentInfo>> RetrieveAllPaymentAsync(int pageNumber, int perPageSize);
    }
}
