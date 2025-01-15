using LowAlt_team_edition.command_line;
using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        DataContext dataContext = new DataContext{
            dataFolder = "data"
        };

        var dataLoader = new DataLoaderService(dataContext.dataFolder);
        (var routes, var flights) = dataLoader.GetData();
        dataContext.Routes = routes;
        dataContext.Flights = flights;

        var interfata = new UserInterface(dataContext);
        interfata.StartInteraction();


        Console.Clear();


        // foreach(var route in routes) {
        //     Console.WriteLine(route);
        // }
        // foreach(var flight in flights) {
        //     Console.WriteLine(flight);n
        // }


        // var accountLoader = new AccountLoaderService("data");
        // var account = accountLoader.GetAccount("admin", "admin", flights);
        // Console.WriteLine(account);
    }
}