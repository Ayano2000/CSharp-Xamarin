using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using PrismApp.DTO;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismApp.ViewModels
{
    public class WeatherInfoViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private IRestService _restService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private bool _isBusy;
        private ILocationService _locationService;

        public Command GetWeatherButtonClicked { get; }
        public Command WeatherButtonClickedCommand { get; }

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService, ILocationService locationService)
        {
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            _restService = restService;
            _locationService = locationService;
            _navigationService = navigationService;
            WeatherButtonClickedCommand = new Command(async () => await GetWeatherInfo());
        }
        public ObservableCollection<CityWeatherViewModel> CityWeatherViewModels
        {
            get => this._cityWeatherModels;
            set
            {
                _cityWeatherModels = value;
                RaisePropertyChanged();
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

        async void ExecuteNavigationCommand() //async if you're awaiting
        {
            await _navigationService.GoBackAsync();
        }

        private async Task GetWeatherInfo()
        {
            IsBusy = true;
            foreach (var city in Configuration.CityNames)
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    string requestUri = _locationService.GetCityQuery(city);
                    // Await result from endpoint query
                    var weatherModel = await _restService.GetWeatherData(requestUri);
                    CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel));
                    Console.WriteLine("API RESOLVED -> " + weatherModel.Title);
                }
                IsBusy = false;
            }

            //Configuration.CityNames.ForEach(async (city) =>
            //{
                //if (!string.IsNullOrWhiteSpace(city))
                //{
                //    string requestUri = Constants.Constants.Endpoint;
                //    requestUri += $"?q={city}";
                //    requestUri += "&units=metric"; // or units=imperial
                //    requestUri += $"&APPID={Constants.Constants.APIKey}";
                //    // Await result from endpoint query
                //    var weatherModel = await _restService.GetWeatherData(requestUri);
                //    CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel));
                //    Console.WriteLine("API RESOLVED -> " + weatherModel.Title);
                //}
                //IsBusy = false;
            //});
        }
    }
}