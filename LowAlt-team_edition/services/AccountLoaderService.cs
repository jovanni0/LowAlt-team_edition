using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class AccountLoaderService : Messages
{
    private AccountFinderService _accountFinderService;
    private ILogger _logger;

    public AccountLoaderService(string accountsFile, ILogger logger)
    {
        _accountFinderService = new AccountFinderService(accountsFile, logger);
        _logger = logger;
    }

    public Passenger? GetAccount(string username, string password, List<Flight> flights)
    {
        MockPassanger? mockAccount = _accountFinderService.GetAccountFromFile(username, password);
        if (mockAccount == null) return null;

        Passenger passenger = GetAccount(mockAccount, flights);
        return passenger;
    }

    private Passenger GetAccount(MockPassanger mockPassanger, List<Flight> flights)
    {
        List<Reservation> reservations = new List<Reservation>();
        List<string> mockReservarions = mockPassanger.Reservations;
        Passenger passenger = new Passenger(
            mockPassanger.Cnp,
            mockPassanger.Username,
            mockPassanger.Password,
            new List<Reservation>(),
            mockPassanger.AccountType == "admin" ? true : false
        );

        foreach(var item in mockReservarions) {
            Reservation? reservation = ComposeReservation(item, passenger, flights);
            if (reservation == null) {
                _logger.LogWarning($"Invalid reservation: {item}");
                continue;
            }

            reservations.Add(reservation);
        }

        passenger.PriorReservations.AddRange(reservations);
        return passenger;
    }

    private Reservation? ComposeReservation(string entry, Passenger passenger, List<Flight> flights) {
        string[] parts = entry.Split("-");
        if (parts.Length < 2) {
            _logger.LogWarning($"Invalid reservation entry: {entry}");
            return null;
        }

        int? seats = ParseGreaterThen_Int(parts[1], 0);
        if (seats == null) {
            _logger.LogWarning($"Invalid number of reserved seats: ${parts[1]}");
            return null;
        }

        Flight? flight = flights.Find(item => item.FlightId == parts[0]);
        if (flight == null) {
            _logger.LogWarning($"Could not find a flight with id {parts[0]}");
            return null;
        }

        Reservation reservation = new Reservation(
            passenger,
            flight,
            (int)seats
        );

        return reservation;
    }
}
