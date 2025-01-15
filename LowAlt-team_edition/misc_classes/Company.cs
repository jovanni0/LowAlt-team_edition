using LowAlt_team_edition.command_line;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition.misc_classes;

public class Company
{
    /// <summary>
    /// the company name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// the fleet of the company
    /// </summary>
    public List<Flight> Fleet { get; init; }

    /// <summary>
    /// the routes if the company
    /// </summary>
    public List<Ruta> Routes { get; set; }

    public readonly DataContext Context;

    public Company(string name, DataContext dataContext)
    {
        Name = name;
        Context = dataContext;
        Fleet = dataContext.Flights;
        Routes = dataContext.Routes;
    }

    public void Begin()
    {
        /// read the data from the files
        var dataLoader = new DataLoaderService(Context.FlightsFile, Context.RoutesFile);
        (var routes, var flights) = dataLoader.GetData();
        Context.Routes = routes;
        Context.Flights = flights;

        /// run the console application
        var interfata = new UserInterface(Context);
        interfata.StartInteraction();

        /// save the data in the files
        AccountWriterService accountsWriter = new AccountWriterService(Context.AccountsFile);
        accountsWriter.WriteAccountsToFile(Context.Passengers);
        FlightWriterService flightsWriter = new FlightWriterService(Context.FlightsFile);
        flightsWriter.WriteFlightsToFile(Context.Flights);
        RouteWriterService routeWriter = new RouteWriterService(Context.RoutesFile);
        routeWriter.WriteRoutesToFile(Context.Routes);
    }
}