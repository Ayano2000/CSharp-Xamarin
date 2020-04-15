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
                if (userCitiesList != null) return userCitiesList;
                else
                {
                    return new List<string>();
                }
            }
            private set { AppSettings.AddOrUpdateValue(nameof(UserCities), JsonConvert.SerializeObject(value)); }
        }

        public bool AddCity(string city)
        {
            var cities = UserCities;
            
            var to_check = city.ToUpper().Trim();
            
            if (cities.Contains(to_check)) return false;
            
            cities.Add(to_check);
            UserCities = cities;
            return true;
        }
        
        public bool RemoveCity(string city)
        {
            var cities = UserCities;
            
            var to_check = city.ToUpper().Trim();
            
            if (!cities.Contains(to_check)) return false;

            cities.Remove(to_check);
            UserCities = cities;
            return true;
        }
    }
}