using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "Airplane")]
    public class Airplane
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "airplane_number")]
        public string AirplaneNumber { get; set; }

        [Column(Name = "airplane_brand_id")]
        public int AirplaneBrandId { get; set; }
    }
}
