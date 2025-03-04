using System.Collections.Generic;
using Newtonsoft.Json;

namespace SD.Responses
{
    public class ServicesModel
    {
        [JsonProperty("status")]
        public bool status { get; set; }

       // [JsonProperty("payload")]
       // public List<ServiceModel> payload { get; set; }
    }
}
