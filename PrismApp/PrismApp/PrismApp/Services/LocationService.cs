using PrismApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System;

namespace PrismApp.Services
{
    public class LocationService : ILocationService
    {
        public async Task<Location> GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            // Long then Lat not other way around, if only that didnt take 20 minutes
            
            return location;
        }
    }
}