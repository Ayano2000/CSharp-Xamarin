using System;
using Prism.Commands;
using Prism.Mvvm;
using PrismApp.DTO;
using PrismApp.Constants;
using PrismApp.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class CityWeatherViewModel : BindableBase
    {
        private readonly IPopupNavigation _popupNavigation;

        private string _weatherConditions;
        private string _location;
        private double _temperature;
        private double _windSpeed;
        private long _humidity;
        private long _sunrise;
        private long _sunset;
        private bool _isAddNewSlide;
        public DelegateCommand RemoveCity => new DelegateCommand(RemoveCityCommand);
        public DelegateCommand ShowAddCityPage => new DelegateCommand(ShowAddCityPageCommand);
        public DelegateCommand About => new DelegateCommand(AboutCommand);

        public CityWeatherViewModel(WeatherModel weather, bool newSlide, IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            
            Location = weather.Title;
            Temperature = weather.Main.Temperature;
            WindSpeed = weather.Wind.Speed;
            Humidity = weather.Main.Humidity;
            Sunrise = weather.Sys.Sunrise;
            Sunset = weather.Sys.Sunset;
            WeatherConditions = Constants.Constants.GetWeatherConditions(weather.Weather[0].Id);
            IsAddNewSlide = newSlide;
        }

        public bool IsAddNewSlide
        {
            get => _isAddNewSlide;
            set
            {
                _isAddNewSlide = value;
                RaisePropertyChanged(nameof(IsAddNewSlide));
            }
        }
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }
        
        public string WeatherConditions
        {
            get => _weatherConditions;
            set
            {
                _weatherConditions = value;
                RaisePropertyChanged(nameof(WeatherConditions));
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                RaisePropertyChanged(nameof(Temperature));
            }
        }

        public double WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                RaisePropertyChanged(nameof(WindSpeed));
            }
        }

        public long Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                RaisePropertyChanged(nameof(Humidity));
            }
        }

        public long Sunrise
        {
            get => _sunrise;
            set
            {
                _sunrise = value;
                RaisePropertyChanged(nameof(Sunrise));
            }
        }

        public long Sunset
        {
            get => _sunset;
            set
            {
                _sunset = value;
                RaisePropertyChanged(nameof(Sunset));
            }
        }

        private void RemoveCityCommand()
        {
            MessagingCenter.Send(Location, "DeleteCity");
        }
        
        private async void ShowAddCityPageCommand()
        { 
            await _popupNavigation.PushAsync(new AddCityView());
        }
        
        private async void AboutCommand()
        {
            await _popupNavigation.PushAsync(new AboutView());
        }
    }
}
