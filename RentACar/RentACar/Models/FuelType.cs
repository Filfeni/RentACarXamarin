using Newtonsoft.Json;

namespace RentACar.Models
{
    public class FuelType
    {
        [JsonProperty("Idcombustible")]
        public int Id { get; set; }

        [JsonProperty("Descripcion")]
        public string Type { get; set; }
    }
}
