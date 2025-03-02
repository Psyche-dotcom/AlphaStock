using AlpaStock.Core.DTOs.Response.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.Repositories.Interface
{
    public interface IPaymentRepo
    {
        Task<PaginatedPaymentInfo> RetrieveAllPaymentAsync(int pageNumber, int perPageSize);
        Task<PaginatedPaymentInfo> RetrieveUserAllPaymentAsync(string userid, int pageNumber, int perPageSize);
    }
}
