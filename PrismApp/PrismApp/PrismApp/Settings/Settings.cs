using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismApp.Settings
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
        
        public static List<string> UserCities
        {
            get
            {
                var UserCitiesJSON = AppSettings.GetValueOrDefault(nameof(UserCities), string.Empty);
                var UserCitiesList = JsonConvert.DeserializeObject<List<string>>(UserCitiesJSON);
                return UserCitiesList;
            }
            
            set => AppSettings.AddOrUpdateValue(nameof(UserCities), JsonConvert.SerializeObject(Configuration.CityNames));
        }
    }
}