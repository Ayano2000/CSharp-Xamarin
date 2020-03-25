using System.Collections.Generic;
using PrismApp.Services;

namespace PrismApp
{
    public static class Configuration
    {
        public static List<string> CityNames { get; set; }

        static Configuration()
        {
            CityNames = Settings.Settings.UserCities;
        }
    }
}
