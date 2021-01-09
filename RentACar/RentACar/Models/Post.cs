﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Direccion { get; set; }
    }
}
