using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismApp.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettings AppSettings
        {
            get
            {
                return (CrossSettings.Current);
            }
        }
        public List<string> UserCities
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