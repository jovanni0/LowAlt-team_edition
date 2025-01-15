using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class RouteReaderService : Messages
{
    private string _pathToFile;

    public RouteReaderService(string pathToFile)
    {
        _pathToFile = pathToFile;
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
                ShowError($"Invalid route entry: {entry}");
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
            ShowError($"Invalid route entry: {entry}");
            return false;
        }

        int? parsedDistance = ParseGreaterThen_Int(parts[1], 0);
        if (parsedDistance is null) {
            ShowError($"Invalid route entry: {entry}");
            return false;
        }

        string[] locations = parts[0].Split("-");
        if (locations.Length < 2) {
            ShowError($"Invalid start and end locations (start-end): {entry}");
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