using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismApp.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettings AppSettings => CrossSettings.Current;

        public List<string> UserCities
        {
            get
            {
                var userCitiesJson = AppSettings.GetValueOrDefault(nameof(UserCities), string.Empty);
                var userCitiesList = JsonConvert.DeserializeObject<List<string>>(userCitiesJson);
                return userCitiesList;
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(UserCities), JsonConvert.SerializeObject(value));   
            }
            
        }
    }
}