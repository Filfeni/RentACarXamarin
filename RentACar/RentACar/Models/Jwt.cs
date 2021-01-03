using Newtonsoft.Json;
using System;

namespace RentACar.Models
{
    public class Jwt
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expiration")]
        public DateTime Expires { get; set; }
    }
}
