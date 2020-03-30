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
        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private bool _addCityButtonIsVisible;

        public Command GetWeatherButtonClicked { get; }
        public Command GetWeatherCommand { get; }

        public DelegateCommand NavigateCommand => new DelegateCommand(ExecuteNavigationCommand);
        public DelegateCommand ShowAddCityPage => new DelegateCommand(ShowAddCityPageCommand);

        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService,
            IQueryService queryService, ISettingsService settingsService)
        {
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            _restService = restService;
            _navigationService = navigationService;
            _queryService = queryService;
            _settingsService = settingsService;
            AddCityButtonIsVisible = false;
            if (settingsService.UserCities.Count() < 3)
            {
                AddCityButtonIsVisible = true;
            }
            GetWeatherCommand = new Command(async () => await GetWeatherInfo());
            GetWeatherCommand.Execute(null);
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
            Console.WriteLine("ShowAddCityPage");
            PopupNavigation.Instance.PushAsync(new AddCity());
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
            await PopupNavigation.Instance.PopAsync();
        }
    }
}