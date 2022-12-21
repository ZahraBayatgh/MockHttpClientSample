using System.Net;
using System.Text.Json.Nodes;

namespace MockHttpClientSample.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UtilityService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> IsValidUrl(string url)
        {
            HttpClient client = _httpClientFactory.CreateClient("Test");
            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                return false;

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
