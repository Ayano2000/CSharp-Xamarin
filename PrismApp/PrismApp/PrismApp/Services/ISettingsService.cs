using System.Collections.Generic;

namespace PrismApp.Services
{
    public interface ISettingsService
    {
        List<string> UserCities { get; }

        void AddCity(string city);
        void RemoveCity(string city);
    }
}
