using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class FlightWriterService
{
    private string _filePath;
    private ILogger _logger;


    public FlightWriterService(string flightsFile, ILogger logger)
    {
        _filePath = flightsFile;
        _logger = logger;
    }


    public void WriteFlightsToFile(List<Flight> flights)
    {
        string database = GenerateDatabase(flights);
        if (!File.Exists(_filePath)) {
            try {
                File.Create(_filePath).Dispose();
            }
            catch (Exception e) {
                _logger.LogWarning($"Could not create file {_filePath}: {e}");
                return;
            }
        }
        try {
            File.WriteAllText(_filePath, database);
        }
        catch (Exception e) {
            _logger.LogWarning($"Could not write to file {_filePath}: {e}");
            return;
        }
    }


    private string GenerateDatabase(List<Flight> flights)
    {
        string header = "FLIGHT_TYPE FLIGHT_ID ROUTE_ID DEPARTURE_TIME FLIGHT_TIME SEATS AVAILABLE_SEATS\n" +
                        "-------------------------------------------------------------------------------";
        foreach (var flight in flights) {
            string entry = StringifyFlight(flight);
            header += $"\n{entry}";
        }

        return header;
    }


    private string StringifyFlight(Flight flight) 
    {
        string flightType = "local";
        if (flight.GetType() == typeof(InternationalFlight)) flightType = "inter";

        return $"{flightType} {flight.FlightId} {flight.Route.IDRuta} {flight.DepartureTime.ToString("HH:mm")} {flight.FlightTime} {flight.Seats} {flight.AvailableSeats}";
    }
}
