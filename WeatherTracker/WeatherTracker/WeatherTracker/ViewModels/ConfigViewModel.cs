using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherTracker.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private string _city;
        private ObservableCollection<string> _cities;

        public ConfigViewModel()
        {
            Cities = new ObservableCollection<string>(Configuration.CityNames);            

            AddCityButtonClicked = new Command(
            execute: () =>
            {;
                Configuration.CityNames.Add(_city);
                Cities.Add(_city);
                Console.WriteLine(_city + " has been added");
                foreach(string name in Configuration.CityNames)
                {
                    Console.WriteLine(name);
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
                OnPropertyChanged(nameof(City));
            }
        }

        public Command AddCityButtonClicked { get; }
    }
}

