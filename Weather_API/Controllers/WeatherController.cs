using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Weather_API.Infrastructure.Repositoy;

namespace Weather_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherInformation _weather;
        public WeatherController(IWeatherInformation weather)
        {
            _weather = weather;
        }
        [HttpGet]

        public async Task<IActionResult> GetWeatherInfo(string location)
        {
            return Ok(await _weather.GetWeatherInfoAsync(location));
        }
    }
}
