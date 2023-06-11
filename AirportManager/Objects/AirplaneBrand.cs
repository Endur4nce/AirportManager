using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "AirplaneBrand")]
    public class AirplaneBrand
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
