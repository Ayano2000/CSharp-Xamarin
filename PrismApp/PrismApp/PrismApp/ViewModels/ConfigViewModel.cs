﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using PrismApp.Constants;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class ConfigViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;
        private string _city;
        private ObservableCollection<string> _cities;

        public DelegateCommand NavigateCommand =>
             _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public ConfigViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Cities = new ObservableCollection<string>(Configuration.CityNames);

            AddCityButtonClicked = new Command(
            execute: () =>
            {
                var userCityInput = _city.Trim();
                if (!Configuration.CityNames.Contains(userCityInput))
                {
                    Configuration.CityNames.Add(userCityInput);
                    Cities.Add(userCityInput);
                    Settings.Settings.UserCities = Configuration.CityNames;
                }
            });
        }

        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
            }
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

        async void ExecuteNavigationCommand()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
