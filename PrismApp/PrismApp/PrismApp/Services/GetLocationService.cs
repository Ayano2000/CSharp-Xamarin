using Newtonsoft.Json;
using PrismApp.DTO;
using System;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PrismApp.Services
{
    class GetLocationService : IRestService
    {
        HttpClient _client;

        public GetLocationService()
        {
            _client = new HttpClient();
        }

        public async Task<string> GenerateQuery()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            string query = Constants.Constants.Endpoint;
            query += $"?lat={location.Latitude}&lon={location.Longitude}";
            query += "&units=metric"; // or units=imperial
            query += $"&APPID={Constants.Constants.APIKey}";
            return (query);
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

            return (weatherData);
        }
    }
}