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

            AddCityButtonClicked = new Command(execute: AddCityToList);
        }

        private void AddCityToList()
        {
            var userCities = _settingsService.UserCities;
            var userCityInput = _city.Trim();

            _settingsService.AddCity(userCityInput);
            Cities.Add(userCityInput);
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
                {
                    return;
                }
                _city = value;
                RaisePropertyChanged(nameof(City));
            }
        }

        public Command AddCityButtonClicked { get; }
    }
}