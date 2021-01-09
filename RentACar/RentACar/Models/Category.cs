using Newtonsoft.Json;

namespace RentACar.Models
{
    public class Category
    {
        [JsonProperty("Idcategoria")]
        public int Id { get; set; }

        [JsonProperty("NombreCategoria")]
        public string Name { get; set; }
    }
}
