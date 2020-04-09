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
            
            MessagingCenter.Unsubscribe<WeatherInfoViewModel>(this, "Addition Successful");
            MessagingCenter.Subscribe<WeatherInfoViewModel>(this, "Addition Successful", 
                (Success) => UpdateAdditionCompletionMessage());
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
        
        private void UpdateAdditionCompletionMessage()
        {
            AdditionCompletionMessage = "City has been added to your list";
        }
        private void AddCityToList()
        {
            var userCityInput = _city.Trim();
            if (_settingsService.UserCities.Contains(userCityInput))
            {
                AdditionCompletionMessage = "City is already on your watch list";
                return;
            }
            MessagingCenter.Send(userCityInput, "NewAdded");
        }
    }
}