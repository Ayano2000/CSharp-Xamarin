using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp.Services
{
    public interface ILocationService
    {
        Task<string> GenerateQuery();
        
        string GetCityQuery(string city);
    }
}
