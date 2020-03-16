using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.DTO
{
    public class CloudsModel
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
}
