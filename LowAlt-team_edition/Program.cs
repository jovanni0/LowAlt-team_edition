using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        List<Flight> ZboruriTest = new List<Flight>
        {
            new LocalFlight("Eu23",new Ruta("Brasov","Budapesta",200,"I2"),new TimeOnly(15,30),120,30,10),
            new InternationalFlight("A23",new Ruta("Timisoara","Bucuresti",160,"I3"),new TimeOnly(15,30),120,30,0)
            
        };
        Passenger pasagerTest = new Passenger("065373234583", "user123", new List<Reservation>(), false);

        RezervariPasageri testfuctieafisare = new RezervariPasageri(pasagerTest, ZboruriTest);
        testfuctieafisare.ZboruriDisponibile();
    }
}