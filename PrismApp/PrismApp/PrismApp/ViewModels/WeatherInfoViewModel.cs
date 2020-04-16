using System;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PrismApp.Controls;
using Rg.Plugins.Popup.Contracts;
using PrismApp.DTO;
using PrismApp.Views;
using Xamarin.Essentials;

namespace PrismApp.ViewModels
{
    public class WeatherInfoViewModel : BindableBase
    {
        private readonly IRestService _restService;
        private readonly INavigationService _navigationService;
        private readonly IQueryService _queryService;
        private readonly ISettingsService _settingsService;
        private readonly ILocationService _locationService;
        private readonly IPopupNavigation _popupNavigation;

        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private Command _getCurrentCityCommand;

        public Command GetWeatherCommand { get; }

        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService,
            IQueryService queryService,
            ISettingsService settingsService, ILocationService locationService, IPopupNavigation popupNavigation)
        {
            Debug.WriteLine("GOT TO CON");
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();

            _restService = restService;
            _navigationService = navigationService;
            _queryService = queryService;
            _settingsService = settingsService;
            _locationService = locationService;
            _popupNavigation = popupNavigation;

            _getCurrentCityCommand = new Command(async () => await GetCurrentCity());

            GetWeatherCommand = new Command(async () => await GetWeatherInfo());

            MessagingCenter.Unsubscribe<string>(this, "NewAdded");
            MessagingCenter.Subscribe<string>(this, "NewAdded", AddCity);

            MessagingCenter.Unsubscribe<string>(this, "DeleteCity");
            MessagingCenter.Subscribe<string>(this, "DeleteCity", DeleteCity);

            Task.Run(async () => await CheckPermissionsAndContinue());
        }

        private async Task CheckPermissionsAndContinue()
        {
            if (await CheckUserConnectivity())
            {
                if (_settingsService.UserCities.Count == 0 || _settingsService.UserCities == null)
                {
                    _getCurrentCityCommand.Execute(null);
                }
                else
                {
                    GetWeatherCommand.Execute(null);
                }
            }
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

        private async Task GetCurrentCity()
        {
            var city = await GetDeviceLocation();
            if (!_settingsService.UserCities.Contains(city.Title)) // prevents duplicate cities from being added
            {
                var success = await AddCityWeatherViewModel(city.Title);
                Console.WriteLine("success is" + success);
                if (success == true)
                {
                    var toAdd = city.Title.ToUpper().Trim();
                    _settingsService.AddCity(toAdd);
                }

                AddDummyCityWeatherViewModel();
            }
        }

        private async Task<DTO.WeatherModel> GetDeviceLocation()
        {
            var location = await _locationService.GetLocation();

            string query = _queryService.GenerateQuery(location.Latitude, location.Longitude);

            var city = await _restService.GetWeatherData(query);
            return city;
        }

        private async Task<bool> CheckUserConnectivity()
        {
            var connection = Connectivity.NetworkAccess;
            if (connection == NetworkAccess.Internet) return true;
            
            await ShowErrorPopup(new Command(async () => await ErrorButtonClicked()),
                "Please check your internet connection");
            
            return false;
        }
        
        private async Task GetWeatherInfo()
        {
            await _popupNavigation.PushAsync(new LoadingPopup(), true);

            foreach (var city in _settingsService.UserCities)
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    await AddCityWeatherViewModel(city);
                }
            }

            AddDummyCityWeatherViewModel();

            await _popupNavigation.PopAsync();
        }

        private async Task<bool> AddCityWeatherViewModel(string city)
        {
            try
            {
                // check that no ViewModel exists for this city
                foreach (var cityNameCollection in CityWeatherViewModels)
                {
                    Console.WriteLine(cityNameCollection.Location);
                    if (cityNameCollection.Location == city) return false; // ViewModel already exists.
                }

                string requestUri = _queryService.GenerateQuery(city);

                // Await result from endpoint query
                var weatherModel = await _restService.GetWeatherData(requestUri);
                CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel, false, _popupNavigation));
                return true;
            }
            catch (Exception e)
            {
                if (e.Message == "Cannot Find City")
                {
                    var viewModel = new AddCityViewModel(_settingsService)
                    {
                        AdditionCompletionMessage = "City Not Found"
                    };
                    Debug.WriteLine("City not found\\n" + e.StackTrace);
                    await _popupNavigation.PushAsync(new AddCityView {BindingContext = viewModel});
                }
                else if (e.Message == "Request Timeout")
                {
                    Debug.WriteLine("Request Timeout\\n" + e.StackTrace);
                    await ShowErrorPopup(new Command(async () => await ErrorButtonClicked()), 
                        "Your request has timed out, Please check your internet connection or try again later");
                }
                else if (e.Message == "Internal Server Error")
                {
                    Debug.WriteLine("Internal Server Error\\n" + e.StackTrace);
                    await ShowErrorPopup(new Command(async () => await ErrorButtonClicked()), 
                        "Internal server error, Please try again later");
                }
                else
                {
                    Debug.WriteLine("Unspecified Error\\n" + e.StackTrace);
                    await ShowErrorPopup(new Command(async () => await ErrorButtonClicked()), 
                        "Something went wrong, Please try again later");
                }
                return false;
            }
        }

        private async Task ShowErrorPopup(Command retryCommand,
            string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = Constants.Constants.DEFAULT_ERROR_MESSAGE;
            }
            
            var viewModel = new ErrorPopupViewModel
            {
                RetryCommand = retryCommand,
                Message = errorMessage
            };

            await _popupNavigation.PushAsync(new ErrorPopupView {BindingContext = viewModel});
        }

        private void AddDummyCityWeatherViewModel()
        {
            var dummy = CityWeatherViewModels.FirstOrDefault(vm => vm.IsAddNewSlide);

            if (dummy != null)
            {
                CityWeatherViewModels.Remove(dummy);
            }
            else
            {
                dummy = new CityWeatherViewModel(new WeatherModel
                {
                    Title = "Dummy",
                    Main = new MainModel
                    {
                        Temperature = 0,
                        Humidity = 0
                    },
                    Sys = new SysModel
                    {
                        Sunrise = 0,
                        Sunset = 0
                    },
                    Visibility = 0,
                    Wind = new WindModel
                    {
                        Speed = 0
                    },
                    Weather = new WeatherDataModel[]
                    {
                        new WeatherDataModel
                        {
                            Id = 800,
                        },
                    }
                }, true, _popupNavigation);
            }

            if (_settingsService.UserCities.Count() != 3)
            {
                CityWeatherViewModels.Add(dummy);
            }
        }

        private void DeleteCity(string cityName)
        {
            foreach (var city in CityWeatherViewModels)
            {
                if (city.Location == cityName)
                {
                    CityWeatherViewModels.Remove(city);
                    _settingsService.RemoveCity(cityName);
                    AddDummyCityWeatherViewModel();
                    break;
                }
            }
        }

        private async void AddCity(string cityName)
        {
            if (cityName == String.Empty) return;

            foreach (var city in CityWeatherViewModels)
            {
                if (city.Location == cityName) return;
            }

            var success = await AddCityWeatherViewModel(cityName);
            if (success == true)
            {
                var toAdd = cityName.ToUpper().Trim();
                _settingsService.AddCity(toAdd);
                await _popupNavigation.PopAllAsync();
            }
            AddDummyCityWeatherViewModel();
        }

        private async Task ErrorButtonClicked()
        {
            await _popupNavigation.PopAsync();
            
            var userHasConnection = await CheckUserConnectivity();
            
            if (!userHasConnection) return;
            
            GetWeatherCommand.Execute(null);
        }
    }
}