using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "Airport")]
    public class Airport
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
