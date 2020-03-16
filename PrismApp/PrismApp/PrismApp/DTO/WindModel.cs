using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.DTO
{
    public class WindModel
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public long Deg { get; set; }
    }
}
