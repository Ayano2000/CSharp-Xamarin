using System.Collections.Generic;

namespace PrismApp.Services
{
    public interface ISettingsService
    {
        List<string> UserCities { get; set; }
    }
}
