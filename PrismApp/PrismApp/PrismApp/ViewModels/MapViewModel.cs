using Xamarin.Essentials;
using Prism.Mvvm;
using System.Threading.Tasks;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class MapViewModel : BindableBase
    {
        private readonly IRestService _restService;
        private readonly IQueryService _queryService;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;

        private Location _location;
        public MapViewModel(INavigationService navigationService, IQueryService queryService, 
            IRestService restService, ISettingsService settingsService)
        {
            _restService = restService;
            _queryService = queryService;
            _navigationService = navigationService;
            _settingsService = settingsService;
            
            new Command(async () => await OpenMapView()).Execute(null);
        }
        
        public Location Location
        {
            get => _location;

            set
            {
                if (_location == value)
                {
                    return;
                }
                _location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }

        public async Task OpenMapView()
        {
            if (_settingsService.UserCities[0] != null)
            {
                string query = _queryService.GenerateQuery(_settingsService.UserCities[0]);
                var cityData = await _restService.GetWeatherData(query);
                _location = new Location(cityData.Coord.Lat, cityData.Coord.Lon);
                var options = new MapLaunchOptions { Name = "Map View PrismApp" };
                await Map.OpenAsync(_location, options);
            }
            else
            {
                var dummyLocation = new Location();
                await Map.OpenAsync(dummyLocation);
            }
        }
    }
}