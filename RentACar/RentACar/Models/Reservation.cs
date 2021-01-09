using Newtonsoft.Json;
using System;

namespace RentACar.Models
{
    public class Reservation
    {
        [JsonProperty("Idreservacion")]
        public int? Id { get; set; }

        [JsonProperty("Idcarro")]
        public int CarId { get; set; }

        [JsonProperty("Idcliente")]
        public int ClientId { get; set; }

        [JsonProperty("IdtipoReservacion")]
        public int ReservationTypeId { get; set; }

        [JsonProperty("FechaSalida")]
        public DateTime StartDate { get; set; }

        [JsonProperty("FechaEntrega")]
        public DateTime EndDate { get; set; }

        [JsonProperty("IdcarroNavigation")]
        public virtual Car Car { get; set; }
    }
}
