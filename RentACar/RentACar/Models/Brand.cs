using Newtonsoft.Json;

namespace RentACar.Models
{
    public class Brand
    {
        [JsonProperty("idmarca")]
        public int Id { get; set; }

        [JsonProperty("marca1")]
        public string Name { get; set; }
    }
}
