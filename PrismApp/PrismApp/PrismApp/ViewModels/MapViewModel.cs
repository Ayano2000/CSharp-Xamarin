using Xamarin.Essentials;
using Prism.Mvvm;
using System.Threading.Tasks;
using PrismApp.Services;

namespace PrismApp.ViewModels
{
    public class MapViewModel : BindableBase
    {
        private IRestService _restService;
        public MapViewModel()
        {
            _restService = new RestService();
            OpenMapView();
        }

        public async Task OpenMapView()
        {
            if (Configuration.CityNames[0] != null)
            {
                string query = Constants.Constants.Endpoint;
                query += $"?q={Configuration.CityNames[0]}";
                query += "&units=metric"; // or units=imperial
                query += $"&APPID={Constants.Constants.APIKey}";
                var CityData = await _restService.GetWeatherData(query);
                var location = new Location(CityData.Coord.Lat, CityData.Coord.Lon);
                var options = new MapLaunchOptions { Name = "Map View PrismApp" };
                await Map.OpenAsync(location, options);
            }
            else
            {
                var dummyLocation = new Location();
                await Map.OpenAsync(dummyLocation);
            }
        }
    }
}