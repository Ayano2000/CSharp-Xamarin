using System;
using System.Collections.Generic;
using System.Text;
using PrismApp.DTO;

namespace PrismApp.Services
{
    public class QueryService : IQueryService
    {
        public string GenerateQuery(string city)
        {
            string query= Constants.Constants.ENDPOINT;
            query+= $"?q={city}";
            query+= "&units=metric"; // or units=imperial
            query+= $"&APPID={Constants.Constants.API_KEY}";
            return (query);
        }

        public string GenerateQuery(double lat, double lon)
        {
            string query = Constants.Constants.ENDPOINT;
            query += $"?lat={lat}&lon={lon}";
            query += "&units=metric"; // or units=imperial
            query += $"&APPID={Constants.Constants.API_KEY}";
            return (query);
        }
        
        public string GenerateQuery(CoordModel coord)
        {
            return GenerateQuery(coord.Lat, coord.Lon);
        }
    }
}
