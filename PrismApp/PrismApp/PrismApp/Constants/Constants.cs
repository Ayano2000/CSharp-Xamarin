using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.Constants
{
    class Constants
    {
        public static string Endpoint = "https://api.openweathermap.org/data/2.5/weather";
        public static string APIKey = "9cb917dd80bbe04c70308bc9a43d3624";
        
        public static string GetWeatherConditions(long WeatherCode)
        {
            if (WeatherCode == null) return "Sunny.json";
            if (WeatherCode > 199 && WeatherCode < 300) return "ThunderStorm.json";
            if (WeatherCode > 299 && WeatherCode < 400) return "Drizzle.json";
            if (WeatherCode > 499 && WeatherCode < 600) return "Rain.json";
            if (WeatherCode == 800) return "Sunny.json";
            if (WeatherCode == 801 || WeatherCode == 802) return "PartlyCloudy.json";
            if (WeatherCode == 803 || WeatherCode == 804) return "Cloudy.json";
            return String.Empty; // Will Handle Error in the VM
        }
    }
}
