using System.Net;
using System.Text.Json.Nodes;

namespace MockHttpClientSample.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UtilityService> _logger;

        public UtilityService(IHttpClientFactory httpClientFactory, ILogger<UtilityService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<bool> IsValidUrl(string url)
        {
            HttpClient client = _httpClientFactory.CreateClient("Test");
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<JsonObject>();
                var message = responseBody!["message"]!.ToString();
                _logger.LogWarning("Request throttled with message: {Message}", message);
                return false;
            }

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
