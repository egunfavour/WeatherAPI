using Weather_API.Core.DTO;

namespace Weather_API.Infrastructure.Repositoy
{
    public interface IWeatherInformation
    {
        Task<ResponseDTO<WeatherResponseDTO>> GetWeatherInfoAsync(string location);
    }
}