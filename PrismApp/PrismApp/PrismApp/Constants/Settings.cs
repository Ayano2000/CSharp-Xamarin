using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismApp.Constants
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return (CrossSettings.Current);
            }
        }
        
        public static string UserCities
        {
            get => AppSettings.GetValueOrDefault(nameof(UserCities), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserCities), JsonConvert.SerializeObject(Configuration.CityNames));
        }
    }
}