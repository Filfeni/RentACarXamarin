using Newtonsoft.Json;

namespace RentACar.Models
{
    public class ReservationType
    {
        [JsonProperty("IdtipoReservacion")]
        public int Id { get; set; }

        [JsonProperty("Alcance")]
        public string Range { get; set; }
    }
}
