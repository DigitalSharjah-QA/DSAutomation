using System;
using Newtonsoft.Json;

namespace SD.Responses
{
    public class UserModel
    {
       
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("expires_in")]
        public int expires_in { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }

        [JsonProperty("issued")]
        public string issued { get; set; }

        [JsonProperty("expires")]
        public string expires { get; set; }

        [JsonProperty("success")]
        public bool success { get; set; }

        [JsonProperty("must_link")]
        public bool must_link { get; set; }

        [JsonProperty("photo")]
        public string photo { get; set; }

        [JsonProperty("name_en")]
        public string name_en { get; set; }

        [JsonProperty("name_ar")]
        public string name_ar { get; set; }

        [JsonProperty("firstname_en")]
        public string firstname_en { get; set; }

        [JsonProperty("firstname_ar")]
        public string firstname_ar { get; set; }

        [JsonProperty("user_type")]
        public int user_type { get; set; }

        [JsonProperty("emirates_id")]
        public string emirates_id { get; set; }
    }
}
