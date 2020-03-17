using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private DelegateCommand _getData;
        private DelegateCommand _mapViewCommand;
        private readonly INavigationService _navigationService;
        private ILocationService _locationService;
        private IRestService _restService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public DelegateCommand GetData =>
            _getData ?? (_getData = new DelegateCommand(ExecuteGetDataCommand));

        public DelegateCommand MapViewCommand =>
            _mapViewCommand ?? (_mapViewCommand = new DelegateCommand(ExecuteMapViewCommand));

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _locationService = new LocationService();
            _restService = new RestService();
            GetDeviceLocation();
        }

        private async void GetDeviceLocation()
        {
            var Query = await _locationService.GenerateQuery();
            var weatherModel = await _restService.GetWeatherData(Query);
            Configuration.CityNames.Add(weatherModel.Title);
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
