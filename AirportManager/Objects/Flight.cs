using System;
using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "Flight")]
    public class Flight
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "airport_id")]
        public int AirportId { get; set; }

        [Column(Name = "airplane_id")]
        public int AirplaneId { get; set; }

        [Column(Name = "departure_date")]
        public DateTime DepartureDate { get; set; }

        [Column(Name = "arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [Column(Name = "available_seats")]
        public int AvailableSeats { get; set; }

        [Column(Name = "price")]
        public int Price { get; set; }
    }
}
