using LinqToDB;


namespace AirportManager.Objects
{
    public class DataContext : LinqToDB.Data.DataConnection
    {
        public DataContext() : base("Server=localhost;Database=NAZVANIE;User ID=root;Password=;")
        {
        }

        public ITable<Airport> Airports => this.GetTable<Airport>();
        public ITable<Flight> Flights => this.GetTable<Flight>();
        public ITable<Airplane> Airplanes => this.GetTable<Airplane>();
        public ITable<AirplaneBrand> AirplaneBrands => this.GetTable<AirplaneBrand>();
    }
}
