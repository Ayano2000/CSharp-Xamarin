using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.DTO;

namespace WeatherTracker.Services
{
    public interface IRestService
    {
        Task<WeatherModel> GetWeatherData(string query);
    }
}
