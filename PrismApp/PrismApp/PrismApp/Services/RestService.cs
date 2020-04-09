using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrismApp.DTO;

namespace PrismApp.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(25);
        }

        public async Task<WeatherModel> GetWeatherData(string query)
        {
            WeatherModel weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherModel>(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
                throw;
            }

            return weatherData;
        }
    }
}