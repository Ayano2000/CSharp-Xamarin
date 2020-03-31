using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PrismApp.Controls;
using PrismApp.DTO;
using PrismApp.Views;
using Rg.Plugins.Popup.Services;

namespace PrismApp.ViewModels
{
    public class WeatherInfoViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private readonly IRestService _restService;
        private readonly INavigationService _navigationService;
        private readonly IQueryService _queryService;
        private readonly ISettingsService _settingsService;
        private readonly ILocationService _locationService;
        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private bool _addCityButtonIsVisible;
        private Command _getCurrentCityCommand;

        public Command GetWeatherButtonClicked { get; }
        public Command GetWeatherCommand { get; }

        public DelegateCommand NavigateCommand => new DelegateCommand(ExecuteNavigationCommand);
        public DelegateCommand ShowAddCityPage => new DelegateCommand(ShowAddCityPageCommand);
        public DelegateCommand About => new DelegateCommand(AboutCommand);
        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService,
            IQueryService queryService, ISettingsService settingsService, ILocationService locationService)
        {
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            _restService = restService;
            _navigationService = navigationService;
            _queryService = queryService;
            _settingsService = settingsService;
            _locationService = locationService;
            AddCityButtonIsVisible = false;
            _getCurrentCityCommand = new Command(async () => await GetCurrentCity());
            if (settingsService.UserCities.Count == 0)
            {
                _getCurrentCityCommand.Execute(null);
            }
            if (settingsService.UserCities.Count() < 3)
            {
                AddCityButtonIsVisible = true;
            }
            GetWeatherCommand = new Command(async () => await GetWeatherInfo());
            GetWeatherCommand.Execute(null);
            MessagingCenter.Unsubscribe<CityWeatherViewModel>(this, "NewAdded");
            MessagingCenter.Unsubscribe<string>(this, "DeleteCity");
            MessagingCenter.Subscribe<CityWeatherViewModel>(this, "NewAdded", (CityData) =>
            {
                if (CityData.Location == String.Empty) return;
                foreach (var city in CityWeatherViewModels)
                {
                    if (city.Location == CityData.Location) return;
                }
                foreach (var city in CityWeatherViewModels)
                {
                    if (city.Location == "Dummy")
                    {
                        CityWeatherViewModels.Remove(city);
                        break;
                    }
                }
                CityWeatherViewModels.Add(CityData);
            });
            MessagingCenter.Subscribe<string>(this, "DeleteCity", (CityName) =>
            {
                foreach (var city in CityWeatherViewModels)
                {
                    if (city.Location == CityName)
                    {
                        CityWeatherViewModels.Remove(city);
                        AddDummyCityWeatherViewModel();
                        break;
                    }
                }
            });
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
        
        private void ShowAddCityPageCommand()
        {
            PopupNavigation.Instance.PushAsync(new AddCity());
        }
        private void AboutCommand()
        {
            PopupNavigation.Instance.PushAsync(new About());
        }
        public bool AddCityButtonIsVisible
        {
            get => _addCityButtonIsVisible;
            set
            {
                _addCityButtonIsVisible = value;
                RaisePropertyChanged(nameof(AddCityButtonIsVisible));
            }
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
            await PopupNavigation.Instance.PushAsync(new LoadingPopup(), true);
            foreach (var city in _settingsService.UserCities)
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    string requestUri = _queryService.GenerateQuery(city);
                    // Await result from endpoint query
                    var weatherModel = await _restService.GetWeatherData(requestUri);
                    CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel));
                }
            }
            if (CityWeatherViewModels.Count() < 3)
            {
                while (CityWeatherViewModels.Count != 3)
                {
                    AddDummyCityWeatherViewModel();
                }
            }
            await PopupNavigation.Instance.PopAsync();
        }

        private void AddDummyCityWeatherViewModel()
        {
            var DummyCity = new CityWeatherViewModel(new WeatherModel
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
                }
            });
            CityWeatherViewModels.Add(DummyCity);
        }
    }
}