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
        private INavigationService _navigationService;
        private ILocationService _locationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public DelegateCommand GetData =>
            _getData ?? (_getData = new DelegateCommand(ExecuteGetDataCommand));

        public DelegateCommand MapViewCommand =>
            _mapViewCommand ?? (_mapViewCommand = new DelegateCommand(ExecuteMapViewCommand));

        public MainPageViewModel(ILocationService locationService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _locationService = locationService;
            GetDeviceLocation();
        }

        private async void GetDeviceLocation()
        {
            var DeviceCity = await _locationService.GetLocation();
            Configuration.CityNames.Add(DeviceCity);
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
