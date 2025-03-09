using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IStockStreamService
    {
         IAsyncEnumerable<string> GetStockMarketLeaderAsync(string leaderType);
    }
}
