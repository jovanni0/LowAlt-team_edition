using Microsoft.Extensions.Hosting;
using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class HostedAppService : IHostedService
{
    private readonly DataContext _context;
    private readonly Company _company;
    private readonly ILogger<HostedAppService> _logger;

    public HostedAppService(DataContext context, Company company, ILogger<HostedAppService> logger)
    {
        _context = context;
        _company = company;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        /// read the data from the files
        var dataLoader = new DataLoaderService(_context.FlightsFile, _context.RoutesFile, _logger);
        (var routes, var flights) = dataLoader.GetData();
        _context.Routes = routes;
        _context.Flights = flights;

        _company.Begin();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        /// save the data in the files
        AccountWriterService accountsWriter = new AccountWriterService(_context.AccountsFile, _logger);
        accountsWriter.WriteAccountsToFile(_context.Passengers);
        FlightWriterService flightsWriter = new FlightWriterService(_context.FlightsFile, _logger);
        flightsWriter.WriteFlightsToFile(_context.Flights);
        RouteWriterService routeWriter = new RouteWriterService(_context.RoutesFile, _logger);
        routeWriter.WriteRoutesToFile(_context.Routes);

        _logger.LogInformation("Data saved succesfully!");

        return Task.CompletedTask;
    }
}
