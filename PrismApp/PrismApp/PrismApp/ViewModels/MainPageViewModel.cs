using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismApp.DTO;
using PrismApp.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocationService _locationService;
        private readonly IQueryService _queryService;
        private readonly IRestService _restService;
        private readonly ISettingsService _settingsService;
        
        private DelegateCommand _getData;
        private DelegateCommand _mapViewCommand;
        private DelegateCommand _showPopupCommand;
        private Command _getCurrentCityCommand;

        public DelegateCommand GetData =>
            _getData ?? (_getData = new DelegateCommand(ExecuteGetDataCommand));

        public DelegateCommand ShowPopupCommand =>
            _showPopupCommand ?? (_showPopupCommand = new DelegateCommand(ExecuteShowPopupCommand));
        
        public DelegateCommand MapViewCommand =>
            _mapViewCommand ?? (_mapViewCommand = new DelegateCommand(ExecuteMapViewCommand));
        
        public MainPageViewModel(INavigationService navigationService, ILocationService locationService, 
            IQueryService queryService, IRestService restService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _locationService = locationService;
            _queryService = queryService;
            _restService = restService;
            _settingsService = settingsService;
            _getCurrentCityCommand = new Command(async () => await GetCurrentCity());
            if (_settingsService.UserCities.Count == 0) // there are no cities being watched by the user.
            {
                Console.WriteLine("INSIDE COMMAND TO GET DEVICE LOCATION");
                _getCurrentCityCommand.Execute(null);
            }
        }

        private async Task GetCurrentCity()
        {
            var city = await GetDeviceLocation();
            if (!_settingsService.UserCities.Contains(city.Title)) // prevents duplicate cities from being added
            {
                _settingsService.UserCities.Add(city.Title);
            }
        }

        private async Task<DTO.WeatherModel> GetDeviceLocation()
        {
            var location = await _locationService.GetLocation();
            string query = _queryService.GenerateQuery(location.Latitude, location.Longitude);
            var city = await _restService.GetWeatherData(query);
            return city;
        }
        async void ExecuteGetDataCommand()
        {
            await _navigationService.NavigateAsync("WeatherInfo");
        }

        async void ExecuteMapViewCommand()
        {
            await _navigationService.NavigateAsync("Map");
        }
        
        private void ExecuteShowPopupCommand()
        {
            PopupNavigation.Instance.PushAsync(new AddCity());
        }
    }
}
