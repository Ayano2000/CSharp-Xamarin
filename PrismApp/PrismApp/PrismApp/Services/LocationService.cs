using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PrismApp.Services
{
    class LocationService : ILocationService
    {
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
    }
}