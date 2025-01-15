using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class RouteReaderService : Messages
{
    private string _pathToFile;
    private ILogger _logger;

    public RouteReaderService(string pathToFile, ILogger logger)
    {
        _pathToFile = pathToFile;
        _logger = logger;
    }

    public List<Ruta> GetRoutesFromFile() {
        IEnumerable<string> lines = File.ReadLines(_pathToFile).Skip(2); // skip the first 2 lines as they are for redability purposes
        return LoadRoutes(lines);
    }

    private List<Ruta> LoadRoutes(IEnumerable<string> lines)
    {
        List<Ruta> routes = new List<Ruta>();

        foreach (var entry in lines) {
            Ruta route;

            if (!TryParseRoute(entry, out route)) {
                _logger.LogWarning($"Invalid route entry: {entry}");
                continue;
            }

            routes.Add(route);
        }

        return routes;
    }

    /// <summary>
    /// parses the route data. returns the success of the conversion.
    /// </summary>
    public bool TryParseRoute(string entry, out Ruta mockRoute) {
        mockRoute = null!;

        string[] parts = entry.Split(" ");

        if (parts.Length < 3) {
            _logger.LogWarning($"Invalid route entry: {entry}");
            return false;
        }

        int? parsedDistance = ParseGreaterThen_Int(parts[1], 0);
        if (parsedDistance is null) {
            _logger.LogWarning($"Invalid route entry: {entry}");
            return false;
        }

        string[] locations = parts[0].Split("-");
        if (locations.Length < 2) {
            _logger.LogWarning($"Invalid start and end locations (start-end): {entry}");
            return false;
        }

        mockRoute = new Ruta(
            locations[0], 
            locations[1], 
            (int)parsedDistance, 
            parts[2]
        );

        return true;
    }
}