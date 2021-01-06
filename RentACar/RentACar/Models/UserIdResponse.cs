using Newtonsoft.Json;

namespace RentACar.Models
{
    public class UserIdResponse
    {
        [JsonProperty("IdUsuario")]
        public int UserId { get; set; }
    }
}
