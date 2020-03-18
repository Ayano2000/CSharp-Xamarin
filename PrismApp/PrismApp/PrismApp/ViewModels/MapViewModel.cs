using Xamarin.Essentials;
using Prism.Mvvm;
using System.Threading.Tasks;
using PrismApp.Services;

namespace PrismApp.ViewModels
{
    public class MapViewModel : BindableBase
    {
        private IRestService _restService;
        private IQueryService _queryService;
        public MapViewModel(IQueryService queryService, IRestService restService)
        {
            _restService = restService;
            _queryService = queryService;
            OpenMapView();
        }

        public async Task OpenMapView()
        {
            if (Configuration.CityNames[0] != null)
            {
                string query = _queryService.GenerateQuery(Configuration.CityNames[0]);
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