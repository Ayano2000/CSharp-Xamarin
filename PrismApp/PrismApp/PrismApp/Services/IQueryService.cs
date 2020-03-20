using System;
using System.Collections.Generic;
using System.Text;
using PrismApp.DTO;

namespace PrismApp.Services
{
    public interface IQueryService
    {
        string GenerateQuery(string city);
        string GenerateQuery(double lat, double lon);
        string GenerateQuery(CoordModel coord);
    }
}
