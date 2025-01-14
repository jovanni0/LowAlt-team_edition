using LowAlt.cli;
using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.command_line;

public class UserInterface
{
    private Passenger user = new Passenger("", "", new List<Reservation>(), false);
    private List<Flight> _fleet;
    private List<Ruta> _routes;


    public UserInterface(List<Flight> fleet, List<Ruta> routes)
    {
        _fleet = fleet;
        _routes = routes;
    }


    public void StartInteraction()
    {
        while (true)
        {
            if (!user.IsLoggedIn) {
                Console.Clear();
                Console.WriteLine("[GUEST PANEL]");
                Console.WriteLine("You are not logged in. Your options will be limited.");
                Console.Write("Continue (Y/n)?: ");
                string option = Console.ReadLine() ?? "y";

                switch(option.ToLower()) {
                    case "y":
                        var interactiunePasageri = new RezervariPasageri(user, _fleet);
                        interactiunePasageri.InteractiunePasageri();
                        break;
                    case "n":
                        var accountManager = new AccountManager(user);
                        accountManager.StartInteraction();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
            else if (user.IsAdmin) {
                Console.Clear();
                Console.WriteLine(
                    "[ADMIN PANEL]\n"+
                    "   (0): LogOut\n" +
                    "   (1): Gestionare Zboruri\n" +
                    "   (2): Optiuni Pasager\n" +
                    "   (3): Rapoarte Si Statistici"
                );
                Console.Write(">> ");

                string? option = Console.ReadLine();

                switch(option) {
                    case null:
                        break;
                    case "0":
                        user = new Passenger("", "", new List<Reservation>(), false);
                        break;
                    case "1":
                        var flightManagement = new FlightManagement(_fleet, _routes);
                        flightManagement.ShowMenu();
                        break;
                    case "2":
                        var passengerManagement = new RezervariPasageri(user, _fleet);
                        passengerManagement.InteractiunePasageri();
                        break;
                    case "3":
                        Console.WriteLine("Not implemented yet.");
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
            else {
                Console.Clear();
                Console.WriteLine(
                    "[USER PANEL]\n"+
                    "   (0): LogOut\n" +
                    "   (1): Optiuni Pasager\n"
                );
                Console.Write(">> ");

                string? option = Console.ReadLine();

                switch(option) {
                    case null:
                        break;
                    case "0":
                        user = new Passenger("", "", new List<Reservation>(), false);
                        break;
                    case "1":
                        var passengerManagement = new RezervariPasageri(user, _fleet);
                        passengerManagement.InteractiunePasageri();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }
    }
}
