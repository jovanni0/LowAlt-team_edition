using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.command_line;

public class UserInterface
{
    private readonly DataContext _dataContext;


    public UserInterface(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public void StartInteraction()
    {
        while (true)
        {
            if (!_dataContext.User.IsLoggedIn) {
                Console.Clear();
                Console.WriteLine("\n[GUEST PANEL]");
                Console.WriteLine("You are not logged in. Your options will be limited.");
                Console.WriteLine("(0): Exit program\n(1): Continue as guest\n(2): Register");
                Console.Write("Select your option: ");
                string option = Console.ReadLine() ?? "1";

                switch(option.ToLower()) {
                    case "0": return;
                    case "1":
                        var interactiunePasageri = new RezervariPasageri(_dataContext);
                        interactiunePasageri.InteractiunePasageri();
                        break;
                    case "2":
                        var accountManager = new AccountManager(_dataContext);
                        accountManager.StartInteraction();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
            else if (_dataContext.User.IsAdmin) {
                Console.Clear();
                Console.WriteLine(
                    "\n[ADMIN PANEL]\n"+
                    "   (9): ShutDown\n"+
                    "   (0): LogOut\n" +
                    "   (1): Gestionare Zboruri\n" +
                    "   (2): Optiuni Pasager\n" +
                    "   (3): Rapoarte Si Statistici"
                );
                Console.Write(">> ");

                string? option = Console.ReadLine();

                switch(option) {
                    case null: break;
                    case "9": return;
                    case "0":
                        _dataContext.User = new Passenger();
                        break;
                    case "1":
                        var flightManagement = new FlightManagement(_dataContext);
                        flightManagement.ShowMenu();
                        break;
                    case "2":
                        var passengerManagement = new RezervariPasageri(_dataContext);
                        passengerManagement.InteractiunePasageri();
                        break;
                    case "3":
                        var rapoarte = new RapoarteSiStatistici(_dataContext);
                        rapoarte.InteractiuneAdministrator();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
            else {
                Console.Clear();
                Console.WriteLine(
                    "\n[USER PANEL]\n"+
                    "   (9): ShutDown\n" +
                    "   (0): LogOut\n" +
                    "   (1): Optiuni Pasager\n"
                );
                Console.Write(">> ");

                string? option = Console.ReadLine();

                switch(option) {
                    case null: break;
                    case "9": return;
                    case "0":
                        _dataContext.User = new Passenger();
                        break;
                    case "1":
                        var passengerManagement = new RezervariPasageri(_dataContext);
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
