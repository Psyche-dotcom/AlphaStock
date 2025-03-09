using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class ApiClient : IApiClient
    {
        private readonly IConfiguration _configuration;

        public ApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IRestResponse<T>> GetAsync<T>(string endpoint)
        {


            var client = new RestClient(endpoint);
            var request = new RestRequest();
            request.AddHeader("Upgrade-Insecure-Requests", "1");

            request.AddQueryParameter("apikey", _configuration["FMP:APIKEY"]);
            var response = await client.ExecuteAsync<T>(request);
            return response;
        }

        public async Task<IRestResponse<T>> PostAsync<T>(string endpoint, object body = null, Dictionary<string, string> headers = null)
        {
            Activity.Current?.SetTag("Endpoint Url :::: ", endpoint);
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
            if (body != null)
            {
                request.AddJsonBody(body);
                Activity.Current?.SetTag($"{endpoint} Req Body :::: ", JsonConvert.SerializeObject(body));
            }


            AddHeadersToRequest(request, headers);

            var response = await client.ExecuteAsync<T>(request);
            Activity.Current?.SetTag($"{endpoint} Response :::: ", response.Content);
            return response;
        }

        public async Task<string> PostAsync3(string endpoint, object body = null, Dictionary<string, string> headers = null)
        {
            Activity.Current?.SetTag("Endpoint Url :::: ", endpoint);

            var client = new RestClient();
            var request = new RestRequest(endpoint, Method.POST);

            if (body != null)
            {
                string jsonBody = JsonConvert.SerializeObject(body);
                request.AddJsonBody(jsonBody);
                Activity.Current?.SetTag($"{endpoint} Req Body :::: ", jsonBody);
            }

            AddHeadersToRequest(request, headers);

            var response = await client.ExecuteAsync(request);

            Activity.Current?.SetTag($"{endpoint} Response :::: ", response.Content);

            if (response.ErrorException != null)
            {
                throw new Exception("Error during request execution", response.ErrorException);
            }

            return response.Content ?? string.Empty; // Return empty string if response is null
        }


        private void AddHeadersToRequest(RestRequest request, Dictionary<string, string> headers)
        {

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
        }


    }
}
