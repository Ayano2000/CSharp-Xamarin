using PrismApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System;

namespace PrismApp.Services
{
    class LocationService : ILocationService
    {
        private IRestService _restService;
        private IQueryService _queryService;

        public LocationService(IRestService restService, IQueryService queryService)
        {
            _restService = restService;
            _queryService = queryService;
        }

        //public async Task<string> GenerateQuery()
        //{
        //    var request = new GeolocationRequest(GeolocationAccuracy.Best);
        //    var location = await Geolocation.GetLocationAsync(request);
        //    string query = Constants.Constants.Endpoint;
        //    query += $"?lat={location.Latitude}&lon={location.Longitude}";
        //    query += "&units=metric"; // or units=imperial
        //    query += $"&APPID={Constants.Constants.APIKey}";
        //    return (query);
        //}
        public async Task<string> GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            // Long then Lat not other way around, if only that didnt take 20 minutes
            string query = _queryService.GenerateQuery(location.Longitude, location.Latitude);
            var city = await _restService.GetWeatherData(query);
            return (city.Title);
        }
    }
}