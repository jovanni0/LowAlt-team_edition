using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Logging;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.File("data/logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        DataContext dataContext = new DataContext{
            AccountsFile = "data/accounts.txt",
            FlightsFile = "data/flights.txt",
            RoutesFile = "data/routes.txt",
            Logger = new LoggerFactory()
                .AddSerilog(logger)
                .CreateLogger("GlobalLogger")
        };
        Company company = new Company("MurderInc", dataContext);

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddSerilog(logger);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(dataContext);
                services.AddSingleton(company);
                services.AddHostedService<HostedAppService>();
            })
            .Build();

        host.Run();
    }
}