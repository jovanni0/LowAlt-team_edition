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
            new InternationalFlight("A23",new Ruta("Timisoara","Bucuresti",160,"I3"),new TimeOnly(15,30),120,30,1)
            
        };
        Passenger pasagerTest = new Passenger("065373234583", "user3", new List<Reservation>(), false);
        List<Passenger> pasageriTest = new List<Passenger>
        {
            pasagerTest,
            new Passenger("7682920262","user2", new List<Reservation>(), true)
        };
        
        RezervariPasageri testfuctieafisare = new RezervariPasageri(pasagerTest, ZboruriTest);
        testfuctieafisare.InteractiunePasageri();
        RapoarteSiStatistici rapoartetestare = new RapoarteSiStatistici(ZboruriTest, pasageriTest);
        rapoartetestare.InteractiuneAdministrator();



    }
}