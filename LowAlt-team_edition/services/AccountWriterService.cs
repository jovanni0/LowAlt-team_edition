using System.Runtime.CompilerServices;
using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class AccountWriterService : Messages
{
    private string _pathToFile;


    public AccountWriterService(string accountsFile)
    {
        _pathToFile = accountsFile;
    }


    public void WriteAccountsToFile(List<Passenger> passengers) {
        string database = GenerateDatabase(passengers);
        if (!File.Exists(_pathToFile)) {
            try {
                File.Create(_pathToFile).Dispose();
            }
            catch (Exception e) {
                Console.WriteLine($"Could not create file {_pathToFile}: {e}");
                return;
            }
        }
        try {
            File.WriteAllText(_pathToFile, database);
        }
        catch (Exception e) {
            Console.WriteLine($"Could not write to file {_pathToFile}: {e}");
        }
    }
    

    private string GenerateDatabase(List<Passenger> passengers)
    {
        string header = "ACCOUNT_TYPE USERNAME PASSWORD CNP FLIGHT_ID-SEATS(0+)\n" +
                        "------------------------------------------------------";
        foreach (var passanger in passengers) {
            MockPassanger mockPassanger = PassengerToMock(passanger);
            string entry = StringifyPassenger(mockPassanger);
            header += $"\n{entry}";
        }

        return header;
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