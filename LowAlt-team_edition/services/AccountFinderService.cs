using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition.services;

public class AccountFinderService : Messages
{
    private string _pathToFile;

    public AccountFinderService(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    /// <summary>
    /// returns a MockPassenger if the account is found, else null
    /// </summary>
    public MockPassanger? GetAccount(string usernameGiven, string passwordGiven)
    {
        using (StreamReader file = new StreamReader(_pathToFile)) {
            file.ReadLine(); file.ReadLine(); // skip first 2 lines

            string? line;
            while ((line = file.ReadLine()) != null) {
                string[] parts = line.Split(" ");

                if (parts.Length < 4) {
                    ShowError($"Invalid account entry:\n -> {line}");
                    continue;
                }

                string accountType = parts[0];
                string username = parts[1];
                string password = parts[2];
                string cnp = parts[3];
                List<string> reservations = parts.Skip(3).ToList();

                if (username == usernameGiven && password != passwordGiven)  {
                    return null;
                }

                MockPassanger account = new MockPassanger(
                    accountType,
                    username,
                    password,
                    cnp,
                    reservations
                );
                return account;
            }
        }

        return null;
    }
}