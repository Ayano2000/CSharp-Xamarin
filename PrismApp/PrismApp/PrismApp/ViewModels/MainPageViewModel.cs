using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private DelegateCommand _getData;
        private DelegateCommand _mapViewCommand;
        private INavigationService _navigationService;
        private ILocationService _locationService;
        private IQueryService _queryService;
        private IRestService _restService;
        private Command _getCurrentCityCommand;
        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public DelegateCommand GetData =>
            _getData ?? (_getData = new DelegateCommand(ExecuteGetDataCommand));

        public DelegateCommand MapViewCommand =>
            _mapViewCommand ?? (_mapViewCommand = new DelegateCommand(ExecuteMapViewCommand));
        
        public MainPageViewModel(INavigationService navigationService, ILocationService locationService, IQueryService queryService, IRestService restService)
        {
            _navigationService = navigationService;
            _locationService = locationService;
            _queryService = queryService;
            _restService = restService;
            _getCurrentCityCommand = new Command(async () => await GetCurrentCity());

            _getCurrentCityCommand.Execute(null);
        }

        private async Task GetCurrentCity()
        {
            var city = await GetDeviceLocation();
            Configuration.CityNames.Add(city.Title);
        }

        private async Task<DTO.WeatherModel> GetDeviceLocation()
        {
            var location = await _locationService.GetLocation();
            string query = _queryService.GenerateQuery(location.Longitude, location.Latitude);
            var city = await _restService.GetWeatherData(query);
            return city;
        }
        async void ExecuteNavigationCommand()
        {
            await _navigationService.NavigateAsync("Config");
        }
        async void ExecuteGetDataCommand()
        {
            await _navigationService.NavigateAsync("WeatherInfo");
        }

        async void ExecuteMapViewCommand()
        {
            await _navigationService.NavigateAsync("Map");
        }
    }
}
