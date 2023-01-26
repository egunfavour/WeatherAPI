using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Weather_API.Core.DTO;
using static Weather_API.Domain.Models.Weather;

namespace Weather_API.Infrastructure.Repositoy
{
    public class WeatherInformation
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration _config;

        public WeatherInformation(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            _config = config;
        }

        public async Task<ResponseDTO<WeatherResponseDTO>> GetWeatherInfoAsync(string location)
        {
            if (string.IsNullOrEmpty(location)) return ResponseDTO<WeatherResponseDTO>.Fail("Location is empty",(int)HttpStatusCode.BadRequest);
            var res = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={location}&APPID={_config.GetConnectionString("ApiKey")}");
            if(!res.IsSuccessStatusCode) 
            {
              return ResponseDTO<WeatherResponseDTO>.Fail("Location not found", (int)HttpStatusCode.NotFound);

            }
            var jsonResult = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<root>(jsonResult);
            var weatherDto = new WeatherResponseDTO
            {
                Temperature = result.main.temp,
                Details = result.weather[0].description,
                Summary = result.weather[0].main,
                Pressure = result.main.pressure.ToString(), 
                Humidity = result.main.humidity.ToString(),
                Sunrise = result.sys.sunrise.ToString(),
                Sunset = result.sys.sunset.ToString(),
                lon = result.coord.lon,
                lat = result.coord.lat
            };
            return 




        }
    }
}
