using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Prism.Mvvm;
using System.Threading.Tasks;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

// using Map = Xamarin.Essentials.Map;

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
            if (_settingsService.UserCities[1] != null)
            {
                IsBusy = true;
                string query = _queryService.GenerateQuery(_settingsService.UserCities[1]);
                var cityData = await _restService.GetWeatherData(query);
                var myPosition = new Position(cityData.Coord.Lat, cityData.Coord.Lon);
                var mapSpan = new MapSpan(myPosition, 0.01, 0.01);
                Map = new Map(mapSpan);
                Map.MapType = MapType.Street;
                IsBusy = false;
            }
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
        
        public bool IsBusy
        {
            get => this._isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }
    }
}
