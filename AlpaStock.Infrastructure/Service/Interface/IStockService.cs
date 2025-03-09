using AlpaStock.Core.DTOs.Response.Stock;
using AlpaStock.Core.DTOs;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStockService
    {
        Task<ResponseDto<IEnumerable<AllStockListResponse>>> GetStockList();
    }
}
