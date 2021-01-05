using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.Models
{
    public class Car
    {
        [JsonProperty("Idcarro")]
        public int CarId { get; set; }

        [JsonProperty("Idcategoria")]
        public int CategoryId { get; set; }

        [JsonProperty("Idmarca")]
        public int BrandId { get; set; }

        [JsonProperty("Idpropietario")]
        public int OwnerId { get; set; }

        [JsonProperty("Idcombustible")]
        public int FuelTypeId { get; set; }

        [JsonProperty("Modelo")]
        public string Model { get; set; }

        [JsonProperty("Año")]
        public string Year { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("Placa")]
        public string LicensePlate { get; set; }

        [JsonProperty("Transmision")]
        public bool Transmission { get; set; }
    }
}
