using Prism.Commands;
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
                if (!Configuration.CityNames.Contains(_city))
                {
                    Configuration.CityNames.Add(_city);
                    Cities.Add(_city);
                    Settings.UserCities = JsonConvert.SerializeObject(Configuration.CityNames);
                    Console.WriteLine(_city + " has been added");
                    foreach (string name in Configuration.CityNames)
                    {
                        Console.WriteLine(name);
                    }
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
