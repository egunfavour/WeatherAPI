using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_API.Core.DTO
{
    public class WeatherResponseDTO
    {
        public double Temperature { get; set; }
        public string Details { get; set; }
        public string Summary { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
    }
}
