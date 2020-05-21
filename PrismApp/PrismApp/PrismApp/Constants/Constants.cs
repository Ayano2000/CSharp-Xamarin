using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.Constants
{
    public static class Constants
    {
        public const string DEFAULT_ERROR_MESSAGE = "Something Went wrong, Please try again later";
        public const string ENDPOINT = "https://api.openweathermap.org/data/2.5/weather";
        public const string API_KEY = "9cb917dd80bbe04c70308bc9a43d3624";
        
        public static string GetWeatherConditions(long weatherCode)
        {
            if (weatherCode > 199 && weatherCode < 300) return "ThunderStorm.json";
            if (weatherCode > 299 && weatherCode < 400) return "Drizzle.json";
            if (weatherCode > 499 && weatherCode < 600) return "Rain.json";
            if (weatherCode == 800) return "Sunny.json";
            if (weatherCode == 801 || weatherCode == 802) return "PartlyCloudy.json";
            if (weatherCode == 803 || weatherCode == 804) return "Cloudy.json";
            return String.Empty; // Will Handle Error in the VM
        }
    }
}
