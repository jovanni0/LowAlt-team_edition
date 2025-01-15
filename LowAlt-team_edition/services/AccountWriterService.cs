using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class AccountWriterService : Messages
{
    private string _pathToFile;


    public AccountWriterService(string pathToFile)
    {
        _pathToFile = pathToFile;
    }


    public void WriteAccountsToFile(List<Passenger> passengers) {
        foreach (var passenger in passengers) {
            MockPassanger mockPassanger = PassengerToMock(passenger);
            WriteMockToFile(mockPassanger);
        }
    }


    private void WriteMockToFile(MockPassanger passenger) 
    {
        string entry = StringifyPassenger(passenger);

        if (!File.Exists(_pathToFile)) {
            File.WriteAllText(
                _pathToFile, 
                "ACCOUNT_TYPE USERNAME PASSWORD CNP FLIGHT_ID-SEATS(0+)\n" +
                "------------------------------------------------------"
            );
        }

        File.AppendAllText(_pathToFile, "\n" + entry);
    }


    private MockPassanger PassengerToMock(Passenger passenger)
    {
        string accountType = "user";
        if (passenger.IsAdmin) accountType = "admin";

        List<string> reservations = new List<string>();
        foreach (var item in passenger.PriorReservations) {
            string reserv = $"{item.TargetFlight.FlightId}-{item}";
            reservations.Add(reserv);
        }

        return new MockPassanger(
            accountType,
            passenger.Username,
            passenger.Password,
            passenger.Cnp,
            reservations
        );
    }


    public string StringifyPassenger(MockPassanger passenger)
    {
        string reservations = string.Join(" ", passenger.Reservations);

        return $"{passenger.AccountType} {passenger.Username} {passenger.Password} {passenger.Cnp} {reservations}";
    }
}