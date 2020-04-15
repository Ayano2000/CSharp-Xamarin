using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PrismApp.Controls;
using Rg.Plugins.Popup.Contracts;
using PrismApp.DTO;
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
        private DelegateCommand _navigateCommand;

        public Command GetWeatherCommand { get; }

        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService, IQueryService queryService, 
            ISettingsService settingsService, ILocationService locationService, IPopupNavigation popupNavigation)
        {
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            
            _restService = restService;
            _navigationService = navigationService;
            _queryService = queryService;
            _settingsService = settingsService;
            _locationService = locationService;
            _popupNavigation = popupNavigation;
            
            Console.WriteLine("CHECK ME " + _settingsService.UserCities);
            _getCurrentCityCommand = new Command(async () => await GetCurrentCity());
            if (_settingsService.UserCities.Count == 0 || _settingsService.UserCities == null)
            {
                _getCurrentCityCommand.Execute(null);
            }
            
            GetWeatherCommand = new Command(async () => await GetWeatherInfo());
            GetWeatherCommand.Execute(null);
            
            MessagingCenter.Unsubscribe<string>(this, "NewAdded");
            MessagingCenter.Subscribe<string>(this, "NewAdded", 
                (CityName) => AddCity(CityName));
            
            MessagingCenter.Unsubscribe<string>(this, "DeleteCity");
            MessagingCenter.Subscribe<string>(this, "DeleteCity", 
                (CityName) => DeleteCity(CityName));
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

        async void ExecuteNavigationCommand()
        {
            await _navigationService.GoBackAsync();
        }
        
        private async Task GetCurrentCity()
        {
            var city = await GetDeviceLocation();
            if (!_settingsService.UserCities.Contains(city.Title)) // prevents duplicate cities from being added
            {
                _settingsService.AddCity(city.Title);
                GetWeatherCommand.Execute(null);
            }
        }

        private async Task<DTO.WeatherModel> GetDeviceLocation()
        {
            var location = await _locationService.GetLocation();
            
            string query = _queryService.GenerateQuery(location.Latitude, location.Longitude);
            
            var city = await _restService.GetWeatherData(query);
            return city;
        }

        private async Task GetWeatherInfo()
        {
            var connection = Connectivity.NetworkAccess;
            if (connection != NetworkAccess.Internet)
            {
                await _popupNavigation.PushAsync(new ErrorPopup());
                MessagingCenter.Send("Please check your internet connection and try again", "ErrorMessage");
                return;
            }
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
                foreach (var CityNameCollection in CityWeatherViewModels)
                {
                    if (CityNameCollection.Location == city) return false; // ViewModel already exists.
                }
                
                string requestUri = _queryService.GenerateQuery(city);
                
                // Await result from endpoint query
                var weatherModel = await _restService.GetWeatherData(requestUri);
                CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel, false, _popupNavigation));
                return true;
            }
            catch (Exception e)
            {
                await _popupNavigation.PushAsync(new ErrorPopup());
                return false;
            }
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

        private void DeleteCity(string CityName)
        {
            foreach (var city in CityWeatherViewModels)
            {
                if (city.Location == CityName)
                {
                    CityWeatherViewModels.Remove(city);
                    _settingsService.RemoveCity(CityName);
                    AddDummyCityWeatherViewModel();
                    break;
                }
            }
        }

        private async void AddCity(string CityName)
        {
            if (CityName == String.Empty) return;
            
            foreach (var city in CityWeatherViewModels)
            {
                if (city.Location == CityName) return;
            }
            
            var success = await AddCityWeatherViewModel(CityName);
            if (success == true)
            {
                _settingsService.AddCity(CityName);
                await _popupNavigation.PopAsync();
            }
            else
            {
                await _popupNavigation.PopAsync();
                await _popupNavigation.PushAsync(new ErrorPopup());
            }
            AddDummyCityWeatherViewModel();
        }
    }
}