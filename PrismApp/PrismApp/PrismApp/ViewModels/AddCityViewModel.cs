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
        private string _additionCompletionMessage;

        public AddCityViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            AddCityButtonClicked = new Command(execute: AddCityToList);
        }
        
        public string AdditionCompletionMessage
        {
            get => _additionCompletionMessage;
            set
            {
                if (_additionCompletionMessage == value)
                {
                    return;
                }
                _additionCompletionMessage = value;
                RaisePropertyChanged(nameof(AdditionCompletionMessage));
            }
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
            var additionSuccessMessage = _settingsService.AddCity(userCityInput);
            if (additionSuccessMessage == true)
            {
                AdditionCompletionMessage = "City successfully added";
            }
            if (additionSuccessMessage == false)
            {
                AdditionCompletionMessage = "City is already on your watch list";
            }
            MessagingCenter.Send(userCityInput, "NewAdded");
        }
    }
}