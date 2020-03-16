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
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public DelegateCommand GetData =>
            _getData ?? (_getData = new DelegateCommand(ExecuteGetDataCommand));

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GetDeviceLocation();
        }

        private async void GetDeviceLocation()
        {
            GetLocationService LocationService = new GetLocationService();
            var Query = await LocationService.GenerateQuery();
            var weatherModel = await LocationService.GetWeatherData(Query);
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
    }
}
