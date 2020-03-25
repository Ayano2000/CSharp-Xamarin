using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PrismApp.Constants;
using PrismApp.Settings;

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
