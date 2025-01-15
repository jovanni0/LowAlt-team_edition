using LowAlt_team_edition.misc_classes;
using Microsoft.Extensions.Logging;

namespace LowAlt_team_edition.services;

public class AccountFinderService
{
    private string _pathToFile;
    private ILogger _logger;


    public AccountFinderService(string pathToFile, ILogger logger)
    {
        _pathToFile = pathToFile;
        _logger = logger;
    }


    /// <summary>
    /// checks the given credentials against the ones found in the file
    /// </summary>
    public MockPassanger? GetAccountFromFile(string usernameGiven, string passwordGiven)
    {
        IEnumerable<string> lines = File.ReadLines(_pathToFile).Skip(2); // skip the first 2 lines as they are for redability purposes
        return FindAccount(lines, usernameGiven, passwordGiven);
    }


    /// <summary>
    /// verify the credentials against the account in the entries.
    /// </summary>
    private MockPassanger? FindAccount(IEnumerable<string> lines, string usernameGiven, string passwordGiven)
    {
        foreach (var entry in lines) {
            MockPassanger account;
            
            if (!TryParseAccount(entry, out account))
            {
                _logger.LogWarning($"Invalid account entry: {entry}");
                continue;
            }

            if (account.Username == usernameGiven && account.Password == passwordGiven) {
                return account;
            }
        }

        return null;
    }

    /// <summary>
    /// parses the account data. returns the success of the conversion.
    /// </summary>
    public bool TryParseAccount(string line, out MockPassanger account)
    {
        account = null!;

        string[] parts = line.Split(" ");
        if (parts.Length < 4) return false;

        string accountType = parts[0];
        string username = parts[1];
        string password = parts[2];
        string cnp = parts[3];
        List<string> reservations = parts.Skip(4).ToList();

        account = new MockPassanger(accountType, username, password, cnp, reservations);
        return true;
    }
}