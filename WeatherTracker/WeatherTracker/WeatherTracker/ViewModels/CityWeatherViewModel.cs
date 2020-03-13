﻿using System;
using System.Collections.Generic;
using System.Text;
using WeatherTracker.DTO;

namespace WeatherTracker.ViewModels
{
    public class CityWeatherViewModel : ViewModelBase
    {
        private string _location;
        private double _temperature;
        private double _windSpeed;
        private long _humidity;
        private long _visibility;
        private long _sunrise;
        private long _sunset;
        public CityWeatherViewModel(WeatherModel weather)
        {
            Location = weather.Title;
            Temperature = weather.Main.Temperature;
            WindSpeed = weather.Wind.Speed;
            Humidity = weather.Main.Humidity;
            Visibility = weather.Visibility;
            Sunrise = weather.Sys.Sunrise;
            Sunset = weather.Sys.Sunset;
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public double WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                OnPropertyChanged(nameof(WindSpeed));
            }
        }

        public long Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }

        public long Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public long Sunrise
        {
            get => _sunrise;
            set
            {
                _sunrise = value;
                OnPropertyChanged(nameof(Sunrise));
            }
        }

        public long Sunset
        {
            get => _sunset;
            set
            {
                _sunset = value;
                OnPropertyChanged(nameof(Sunset));
            }
        }
    }
}
