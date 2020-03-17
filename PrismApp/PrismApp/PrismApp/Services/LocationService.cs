using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PrismApp.Services
{
    public class LocationService : ILocationService
    {
        //todo - Arata: move the query parts to its own service, only Location specific functionality should be called here
        
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

        public string GetCityQuery(string city)
        {
            string requestUri = Constants.Constants.Endpoint;
            requestUri += $"?q={city}";
            requestUri += "&units=metric"; // or units=imperial
            requestUri += $"&APPID={Constants.Constants.APIKey}";
            
            return requestUri;
        }
    }
}