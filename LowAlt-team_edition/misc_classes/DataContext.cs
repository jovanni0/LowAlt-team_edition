namespace LowAlt_team_edition.misc_classes;

public class DataContext
{
    public List<Flight> Flights = new List<Flight>();

    public List<Ruta> Routes = new List<Ruta>();

    public Passenger User = new Passenger();

    public string dataFolder = string.Empty;

    public override string ToString()
    {
        string routes = string.Empty;
        foreach (var route in Routes) {
            routes += $"\n{route}";
        }

        string flights = string.Empty;
        foreach (var flight in Flights) {
            flights += $"\n| {flight}";
        }

        return $"---DATA CONTEXT--------------------------------\n" +
               $"| USER: {User}\n" + 
               $"| ROUTES: {routes}\n" + 
               $"| FLIGHTS: {flights}\n" +
               $"-----------------------------------------------";
    }
}
