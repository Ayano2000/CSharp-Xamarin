using System;
using System.Collections.Generic;
using System.Text;
using PrismApp.DTO;

namespace PrismApp.Services
{
    public interface IQueryService
    {
        string GenerateQuery(string city);
        string GenerateQuery(double lon, double lat);
        
        string GenerateQuery(CoordModel coord);
    }
}
