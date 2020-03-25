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
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocationService _locationService;
        private readonly IQueryService _queryService;
        private readonly IRestService _restService;
        private DelegateCommand _navigateCommand;
        private DelegateCommand _getData;
        private DelegateCommand _mapViewCommand;
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
            if (Configuration.CityNames.Count == 0) // there are no cities being watched by the user.
            {
                Console.WriteLine("INSIDE COMMAND TO GET DEVICE LOCATION");
                _getCurrentCityCommand.Execute(null);
            }
        }

        private async Task GetCurrentCity()
        {
            var city = await GetDeviceLocation();
            if (!Configuration.CityNames.Contains(city.Title)) // prevents duplicate cities from being added
            {
                Configuration.CityNames.Add(city.Title);
            }
        }

        private async Task<DTO.WeatherModel> GetDeviceLocation()
        {
            var location = await _locationService.GetLocation();
            string query = _queryService.GenerateQuery(location.Latitude, location.Longitude);
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
