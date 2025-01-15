using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class DataLoaderService
{
    private FlightReaderService _flightReaderService;
    private RouteReaderService _routeReaderService;
    private ILogger _logger;
    

    public DataLoaderService(string filghtsFilePath, string routesFilePath, ILogger logger)
    {
        _flightReaderService = new FlightReaderService(filghtsFilePath, logger);
        _routeReaderService = new RouteReaderService(routesFilePath, logger);
        _logger = logger;
    }


    public (List<Ruta>, List<Flight>) GetData()
    {
        List<Ruta> routes = _routeReaderService.GetRoutesFromFile();
        List<MockFlight> mockFlights = _flightReaderService.GetFlightsFromFile();
        List<Flight> flights = new List<Flight>();

        foreach (var mockFlight in mockFlights) {
            string routeId = mockFlight.routeId;

            Ruta? actualRoute = routes.Find(item => item.IDRuta == routeId);

            if (actualRoute == null) {
                _logger.LogWarning($"Unable to find route with id {routeId}");
                continue;
            }

            Flight actualFlight;
            switch(mockFlight.flightType) {
                case "local": actualFlight = new LocalFlight(mockFlight, actualRoute); break;
                case "inter": actualFlight = new InternationalFlight(mockFlight, actualRoute); break;
                default:
                    _logger.LogWarning($"Invalid flight type: {mockFlight.flightType}");
                    continue;
            }

            flights.Add(actualFlight);
        }

        return (routes, flights);
    }

}
