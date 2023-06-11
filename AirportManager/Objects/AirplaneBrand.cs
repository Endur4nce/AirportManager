using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "AirplaneBrand")]
    public class AirplaneBrand
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
