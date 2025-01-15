using LowAlt_team_edition.command_line;
using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        DataContext dataContext = new DataContext{
            AccountsFile = "data/accounts.txt",
            FlightsFile = "data/flights.txt",
            RoutesFile = "data/routes.txt"
        };

        /// read the data from the files
        var dataLoader = new DataLoaderService(dataContext.FlightsFile, dataContext.RoutesFile);
        (var routes, var flights) = dataLoader.GetData();
        dataContext.Routes = routes;
        dataContext.Flights = flights;

        /// run the console application
        var interfata = new UserInterface(dataContext);
        interfata.StartInteraction();

        /// save the data in the files
        AccountWriterService accountsWriter = new AccountWriterService(dataContext.AccountsFile);
        accountsWriter.WriteAccountsToFile(dataContext.Passengers);
        FlightWriterService flightsWriter = new FlightWriterService(dataContext.FlightsFile);
        flightsWriter.WriteFlightsToFile(dataContext.Flights);
        RouteWriterService routeWriter = new RouteWriterService(dataContext.RoutesFile);
        routeWriter.WriteRoutesToFile(dataContext.Routes);
    }
}