using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.DTO
{
    public class CoordModel
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}
