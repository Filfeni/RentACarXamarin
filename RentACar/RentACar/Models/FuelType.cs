using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
