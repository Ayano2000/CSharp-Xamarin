using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
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
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<WeatherModel> GetWeatherData(string query)
        {
            WeatherModel weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NotFound)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherModel>(data);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cannot Find City", new Exception(response.StatusCode.ToString()));
                    }
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        throw new Exception("Internal Server Error", new Exception(response.StatusCode.ToString()));
                    }

                    if (response.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        throw new Exception("Request Timeout", new Exception(response.StatusCode.ToString()));
                    }
                    if (response.StatusCode != HttpStatusCode.NotFound &&
                        response.StatusCode != HttpStatusCode.InternalServerError)
                    {
                        throw new Exception("Something Went Wrong", new Exception(response.StatusCode.ToString()));
                    }
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