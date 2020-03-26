using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using PrismApp.Services;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class AddCityViewModel : BindableBase
    {
        private ISettingsService _settingsService;
        private string _city;
        private ObservableCollection<string> _cities;
        
        public AddCityViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            Cities = new ObservableCollection<string>(_settingsService.UserCities);

            AddCityButtonClicked = new Command(
            execute: () =>
            {
                var userCities = _settingsService.UserCities;
                var userCityInput = _city.Trim();
                if (!_settingsService.UserCities.Contains(userCityInput))
                {
                    userCities.Add(userCityInput);
                    Cities.Add(userCityInput);
                    _settingsService.UserCities = userCities;
                }
            });
        }

        public ObservableCollection<string> Cities
        {
            get => _cities;
            set => _cities = value;
        }

        public string City
        {
            get => _city;
            set
            {
                if (_city == value)
                    return;
                _city = value;
                RaisePropertyChanged(nameof(City));
            }
        }

        public Command AddCityButtonClicked { get; }
    }
}
