using Microsoft.Extensions.Logging;
using MockHttpClientSample.Services;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http.Json;

namespace MockHttpClientSample.Test
{
    public class UtilityServiceTest
    {
        private readonly UtilityService _utilityService;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private readonly Mock<ILogger<UtilityService>> _loggerMock = new();
        private readonly MockHttpMessageHandler _httpMessageHandler = new();

        public UtilityServiceTest()
        {
            _utilityService = new UtilityService(_httpClientFactoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task IsValidUrl_When_Url_Is_Not_Valid_Return_False()
        {
            // Arrange
            var url = "https://www.test.com";

            // Mock the HttpClient response
            _httpMessageHandler.When(url)
                .Respond(HttpStatusCode.Forbidden, JsonContent.Create(new
                {
                }));

            _httpClientFactoryMock.Setup(x => x.CreateClient("Test"))
                .Returns(new HttpClient(_httpMessageHandler));

            // Act
            var resultAction = await _utilityService.IsValidUrl(url);

            // Assert
            Assert.False(resultAction);
        }

        [Fact]
        public async Task IsValidUrl_When_Url_Is_Not_Valid_Return_True()
        {
            // Arrange
            var url = "https://www.test.com";

            // Mock the HttpClient response
            _httpMessageHandler.When(url)
                .Respond(HttpStatusCode.OK, JsonContent.Create(new
                {
                }));

            _httpClientFactoryMock.Setup(x => x.CreateClient("Test"))
                .Returns(new HttpClient(_httpMessageHandler));

            // Act
            var resultAction = await _utilityService.IsValidUrl(url);

            // Assert
            Assert.True(resultAction);
        }
    }
}