using System;
using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class DataLoaderService : Messages
{
    FlightReaderService _flightReaderService;
    RouteReaderService _routeReaderService;


    public DataLoaderService(string dataFolder)
    {
        _flightReaderService = new FlightReaderService($"{dataFolder}/flights.txt");
        _routeReaderService = new RouteReaderService($"{dataFolder}/routes.txt");
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
                ShowError($"Unable to find route with id {routeId}");
                continue;
            }

            Flight actualFlight;
            switch(mockFlight.flightType) {
                case "local": actualFlight = new LocalFlight(mockFlight, actualRoute); break;
                case "inter": actualFlight = new InternationalFlight(mockFlight, actualRoute); break;
                default:
                    Console.WriteLine($"Invalid flight type: {mockFlight.flightType}");
                    continue;
            }

            flights.Add(actualFlight);
        }

        return (routes, flights);
    }

}
