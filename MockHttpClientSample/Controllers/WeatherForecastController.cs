using Microsoft.AspNetCore.Mvc;
using MockHttpClientSample.Services;

namespace MockHttpClientSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IUtilityService _utilityService;

        public WeatherForecastController( IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(string url)
        {
           var result=_utilityService.IsValidUrl(url);
            return Ok(result);
        }
    }
}