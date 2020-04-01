using System.Collections.Generic;

namespace PrismApp.Services
{
    public interface ISettingsService
    {
        List<string> UserCities { get; }

        bool AddCity(string city);
        bool RemoveCity(string city);
    }
}
