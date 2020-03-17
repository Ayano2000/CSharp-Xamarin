using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.Services
{
    public interface IQueryService
    {
        string GenerateQuery(string city);
        string GenerateQuery(double lon, double lat);
    }
}
