using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class FlightReaderService : Messages
{
    private string _pathToFile;

    public FlightReaderService(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    public List<MockFlight> GetFlights()
    {
        List<MockFlight> fleet = new List<MockFlight>();

        using (StreamReader file = new StreamReader(_pathToFile)) {
            file.ReadLine(); file.ReadLine(); // skip first 2 lines

            string? line;
            while ((line = file.ReadLine()) != null) {
                string[] parts = line.Split(" ");

                if (parts.Length < 7) {
                    ShowError($"Invalid flight entry:\n -> {line}");
                    continue;
                }

                string flightType = parts[0];
                string flightId = parts[1];
                string routeId = parts[2];
                TimeOnly? departureTime = ParseTimeOnly(parts[3]);
                int? flightTime = ParseInt(parts[4]);
                int? seats = ParseInt(parts[5]);
                int? availableSeats = ParseInt(parts[6]);

                if (departureTime == null || flightTime == null || 
                seats == null || availableSeats == null) {
                    ShowError($"Invalid flight entry:\n -> {line}");
                    continue;
                }
                if (flightType != "local" && flightType != "inter") {
                    ShowError($"Invalid flight type {flightType} from:\n -> {line}");
                    continue;
                }

                MockFlight flight = new MockFlight(
                    flightType, 
                    flightId, 
                    routeId, 
                    (TimeOnly)departureTime, 
                    (int)flightTime, 
                    (int)seats, 
                    (int)availableSeats
                );
                fleet.Add(flight);
            }
        }

        return fleet;
    }
}
