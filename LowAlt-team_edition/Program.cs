using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        // RouteReaderService routeReader = new RouteReaderService("data/routes.txt");
        // var routes = routeReader.GetRoutesFromFile();
        // foreach(var ruat in routes) {
        //     Console.WriteLine(ruat);
        // }

        // FlightReaderService flightReader = new FlightReaderService("data/flights.txt");
        // var flights = flightReader.GetFlightsFromFile();
        // foreach(var ruat in routes) {
        //     Console.WriteLine(ruat);
        // }

        // AccountFinderService accountFinder = new AccountFinderService("data/accounts.txt");
        // var passanger = accountFinder.GetAccountFromFile("admin", "admin");
        // if (passanger is null) Console.WriteLine($"no account with credentials admin, admin.");
        // else Console.WriteLine("Account found");

        // passanger = accountFinder.GetAccountFromFile("admin", "adm1n");
        // if (passanger is null) Console.WriteLine($"no account with credentials admin, admin.");
        // else Console.WriteLine("Account found");

        // passanger = accountFinder.GetAccountFromFile("test2", "1234");
        // if (passanger is null) Console.WriteLine($"no account with credentials admin, admin.");
        // else Console.WriteLine("Account found");

        var account = new MockPassanger(
            "admin",
            "Ioan",
            "sesamdeschidete",
            "1234567890123",
            new List<string>{ "01-2", "02-03"}
        );
        var accountWriterService = new AccountWriterService("data/newaccounts.txt");
        accountWriterService.WriteAccountToFile(account);
    }
}