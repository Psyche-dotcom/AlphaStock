using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IApiClient
    {
        Task<string> PostAsync3(string endpoint, object body = null, Dictionary<string, string> headers = null);
        Task<IRestResponse<T>> PostAsync<T>(string endpoint, object body = null, Dictionary<string, string> headers = null);
        Task<IRestResponse<T>> GetAsync<T>(string endpoint);
    }
}
