using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class AccountWriterService : Messages
{
    private string _pathToFile;


    public AccountWriterService(string pathToFile)
    {
        _pathToFile = pathToFile;
    }


    public void WriteAccountToFile(MockPassanger passenger) {
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

    public string StringifyPassenger(MockPassanger passenger)
    {
        string reservations = string.Join(" ", passenger.Reservations);

        return $"{passenger.AccountType} {passenger.Username} {passenger.Password} {passenger.Cnp} {reservations}";
    }
}