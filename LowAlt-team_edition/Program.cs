using LowAlt_team_edition.command_line;
using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        var dataLoader = new DataLoaderService("data");
        (var routes, var flights) = dataLoader.GetData();
        // foreach(var route in routes) {
        //     Console.WriteLine(route);
        // }
        // foreach(var flight in flights) {
        //     Console.WriteLine(flight);
        // }

        // var interfata = new UserInterface(ZboruriTest, rute);
        // interfata.StartInteraction();

        var accountLoader = new AccountLoaderService("data");
        var account = accountLoader.GetAccount("admin", "admin", flights);
        Console.WriteLine(account);
    }
}