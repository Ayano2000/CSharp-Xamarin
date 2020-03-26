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
            private set { AppSettings.AddOrUpdateValue(nameof(UserCities), JsonConvert.SerializeObject(value)); }
        }

        public void AddCity(string city)
        {
            var cities = UserCities;

            if (cities.Contains(city)) return;

            cities.Add(city);
            UserCities = cities;
        }
        
        public void RemoveCity(string city)
        {
            // throw new NotImplementedException();
        }
    }
}