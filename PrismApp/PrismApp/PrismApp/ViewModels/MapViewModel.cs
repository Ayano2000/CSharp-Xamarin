using Prism.Mvvm;
using System.Threading.Tasks;
using Prism.Navigation;
using PrismApp.Controls;
using PrismApp.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace PrismApp.ViewModels
{
    public class MapViewModel : BindableBase
    {
        private readonly IRestService _restService;
        private readonly IQueryService _queryService;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private Map _map;
        private bool _isBusy;

        public MapViewModel(INavigationService navigationService, IQueryService queryService, 
            IRestService restService, ISettingsService settingsService)
        {
            _restService = restService;
            _queryService = queryService;
            _navigationService = navigationService;
            _settingsService = settingsService;
            new Command(async () => await SetMapPosition()).Execute(null);
        }
        
        public async Task SetMapPosition()
        {
            await PopupNavigation.Instance.PushAsync(new LoadingPopup() {ProcessDescription = "Loading..."}, true);
            
            if (_settingsService.UserCities[1] != null)
            {
                string query = _queryService.GenerateQuery(_settingsService.UserCities[1]);
                var cityData = await _restService.GetWeatherData(query);
                var myPosition = new Position(cityData.Coord.Lat, cityData.Coord.Lon);
                var mapSpan = new MapSpan(myPosition, 0.01, 0.01);
                Map = new Map(mapSpan);
                Map.MapType = MapType.Street;
            }

            await PopupNavigation.Instance.PopAsync();
        }

        public Map Map
        {
            get => _map;

            set
            {
                if (_map == value)
                {
                    return;
                }
                _map = value;
                RaisePropertyChanged(nameof(Map));
            }
        }
    }
}
