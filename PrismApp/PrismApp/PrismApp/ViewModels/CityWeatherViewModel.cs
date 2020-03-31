using Prism.Commands;
using Prism.Mvvm;
using PrismApp.DTO;
using PrismApp.Services;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class CityWeatherViewModel : BindableBase
    {
        private string _location;
        private double _temperature;
        private double _windSpeed;
        private long _humidity;
        private long _visibility;
        private long _sunrise;
        private long _sunset;
        private ISettingsService _settingsService;
        public DelegateCommand RemoveCity => new DelegateCommand(RemoveCityCommand);

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
        
        private void RemoveCityCommand()
        {
            MessagingCenter.Send(Location, "DeleteCity");
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

        public long Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                RaisePropertyChanged(nameof(Visibility));
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
    }
}
