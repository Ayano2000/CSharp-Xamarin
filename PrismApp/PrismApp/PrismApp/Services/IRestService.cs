using PrismApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp.Services
{
    public interface IRestService
    {
        Task<WeatherModel> GetWeatherData(string query);

    }
}
