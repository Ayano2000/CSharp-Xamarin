using System;
using Prism.Mvvm;
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
        private void AddCityToList()
        {
            var userCityInput = _city.ToUpper().Trim();
            if (_settingsService.UserCities.Contains(userCityInput))
            {
                AdditionCompletionMessage = "City is already on your watch list";
                return;
            }
            MessagingCenter.Send(userCityInput, "NewAdded");
        }
    }
}