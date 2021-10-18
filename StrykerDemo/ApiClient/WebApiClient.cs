using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StrykerDemo.ApiClient
{
    public class WebApiClient
    {
        private readonly HttpClient _httpClient;

        public WebApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetData()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/data");

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
