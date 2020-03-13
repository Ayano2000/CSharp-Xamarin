using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using WeatherTracker.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WeatherTracker.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        //private WeatherModel _weatherModel;
        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private IRestService _restService;
        private bool _isBusy = false;

        public MainPageViewModel()
        {
            this._restService = new RestService();
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            WeatherButtonClickedCommand = new Command(async () => await GetWeatherInfo());
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CityWeatherViewModel> CityWeatherViewModels
        {
            get => this._cityWeatherModels;
            set
            {
                _cityWeatherModels = value;
                OnPropertyChanged();
            }
        }

        public Command WeatherButtonClickedCommand { get; }
        private async Task GetWeatherInfo()
        {
            IsBusy = true;

            Configuration.CityNames.ForEach(async (city) =>
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    string requestUri = Constants.Constants.Endpoint;
                    requestUri += $"?q={city}";
                    requestUri += "&units=metric"; // or units=imperial
                    requestUri += $"&APPID={Constants.Constants.APIKey}";
                    // Await result from endpoint query
                    var weatherModel = await _restService.GetWeatherData(requestUri);
                    CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel));
                }
                IsBusy = false;
            });

        }
    }
}