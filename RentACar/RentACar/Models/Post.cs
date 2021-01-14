using Newtonsoft.Json;

namespace RentACar.Models
{
    public class Post
    {
        [JsonProperty("Idpost")]
        public int Id { get; set; }
        
        [JsonProperty("Idcarro")]
        public int CarId { get; set; }
        
        [JsonProperty("Descripcion")]
        public string Description { get; set; }
        
        [JsonProperty("Precio")]
        public decimal Price { get; set; }
        
        [JsonProperty("Direccion")]
        public string Address { get; set; }

        [JsonProperty("idcarroNavigation")]
        public virtual Car CarNavigation { get; set; }
    }
}
