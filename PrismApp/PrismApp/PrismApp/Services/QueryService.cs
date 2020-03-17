using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.Services
{
    public class QueryService : IQueryService
    {
        public string GenerateQuery(string city)
        {
            string query= Constants.Constants.Endpoint;
            query+= $"?q={city}";
            query+= "&units=metric"; // or units=imperial
            query+= $"&APPID={Constants.Constants.APIKey}";
            return (query);
        }

        public string GenerateQuery(double lon, double lat)
        {
            string query = Constants.Constants.Endpoint;
            query += $"?lat={lat}&lon={lon}";
            query += "&units=metric"; // or units=imperial
            query += $"&APPID={Constants.Constants.APIKey}";
            return (query);
        }
    }
}
