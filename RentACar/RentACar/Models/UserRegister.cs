using Newtonsoft.Json;

namespace RentACar.Models
{
    public class UserRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        [JsonProperty("Nombre")]
        public string FirstName { get; set; }
        
        [JsonProperty("Apellido")]
        public string LastName { get; set; }
        
        [JsonProperty("Telefono")]
        public string PhoneNumber { get; set; }
    }
}
