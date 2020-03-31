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
        private IRestService _restService;
        private IQueryService _queryService;
        private string _city;
        private ObservableCollection<string> _cities;

        public AddCityViewModel(ISettingsService settingsService, IRestService restService, IQueryService queryService)
        {
            _settingsService = settingsService;
            _restService = restService;
            _queryService = queryService;
            Cities = new ObservableCollection<string>(_settingsService.UserCities);
            AddCityButtonClicked = new Command(execute: AddCityToList);
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
        
        private async void AddCityToList()
        {
            var userCities = _settingsService.UserCities;
            var userCityInput = _city.Trim();
            _settingsService.AddCity(userCityInput);
            MessagingCenter.Send(userCityInput, "NewAdded");
        }
    }
}