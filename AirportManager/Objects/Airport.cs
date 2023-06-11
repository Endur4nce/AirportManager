using LinqToDB.Mapping;

namespace AirportManager.Objects
{
    [Table(Name = "Airport")]
    public class Airport
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
