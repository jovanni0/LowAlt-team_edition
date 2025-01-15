using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class FlightReaderService : Messages
{
    private string _pathToFile;
    private ILogger _logger;

    public FlightReaderService(string pathToFile, ILogger logger)
    {
        _pathToFile = pathToFile;
        _logger = logger;
    }


    public List<MockFlight> GetFlightsFromFile()
    {
        IEnumerable<string> lines = File.ReadLines(_pathToFile).Skip(2); // skip the first 2 lines as they are for redability purposes
        return LoadFlights(lines);
    }


    private List<MockFlight> LoadFlights(IEnumerable<string> lines)
    {
        List<MockFlight> fleet = new List<MockFlight>();

        foreach (var entry in lines) {
            MockFlight flight;

            if (!TryParseFlight(entry, out flight)) {
                _logger.LogWarning($"Invalid flight entry: {entry}");
                continue;
            }

            fleet.Add(flight);
        }

        return fleet;
    }

    /// <summary>
    /// parses the flight data. returns the success of the conversion.
    /// </summary>
    public bool TryParseFlight(string entry, out MockFlight flight)
    {
        flight = null!;

        string[] parts = entry.Split(" ");

        if (parts.Length < 7) {
            _logger.LogWarning($"Invalid flight entry:\n -> {entry}");
            return false;
        }

        string flightType = parts[0];
        string flightId = parts[1];
        string routeId = parts[2];
        TimeOnly? departureTime = ParseTimeOnly(parts[3]);
        int? flightTime = ParseGreaterThen_Int(parts[4], 0);
        int? seats = ParseGreaterThen_Int(parts[5], 0);
        int? availableSeats = ParseGreaterOrEqualTo_Int(parts[6], 0);

        if (departureTime == null || flightTime == null || 
        seats == null || availableSeats == null) {
            _logger.LogWarning($"Invalid flight entry:\n -> {entry}");
            return false;
        }
        if (flightType != "local" && flightType != "inter") {
            _logger.LogWarning($"Invalid flight type {flightType} from:\n -> {entry}");
            return false;
        }

        flight = new MockFlight(
            flightType, 
            flightId, 
            routeId, 
            (TimeOnly)departureTime, 
            (int)flightTime, 
            (int)seats, 
            (int)availableSeats
        );

        return true;
    }
}
