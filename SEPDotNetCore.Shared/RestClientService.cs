using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace SEPDotNetCore.Shared
{
    public class RestClientService : IHttpClientService
    {
        private readonly RestClient _restClient;

        public RestClientService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<T> SendAsync<T>(string url, EnumHttpMethod method, object? data = null)
        {
            RestRequest request = new RestRequest(url, (Method)method);
            if (data != null)
            {
                var jsonStr = JsonConvert.SerializeObject(data);
                request.AddJsonBody(jsonStr);
            }

            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content!;
                return JsonConvert.DeserializeObject<T>(responseString)!;
            }
            return default!;
        }
    }
}
