using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt.cli;

public class AccountManager : Messages
{
    private Passenger _user;
    private string _thisPath = "ACCOUNT MANAGEMENT";
    
    
    public AccountManager(Passenger user)
    {
        _user = user;
    }

    
    /// <summary>
    /// starts the interaction with the user.
    /// </summary>
    /// <returns>(valid_account, is_admin)</returns>
    public void StartInteraction()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                $"[{_thisPath}]\n" +
                "   (0): exit\n" +
                "   (1): log in\n" +
                "   (2): sign up"
            );
            Console.Write(">> ");
            var option = Console.ReadLine() ?? "0";

            switch (option)
            {
                case "0": return;
                case "1": LogIn(); break;
                case "2": SignUp(); break;
            }
        }
    }

    
    /// <summary>
    /// checks the credentials
    /// </summary>
    /// <returns>(is_valid_account, is_admin)</returns>
    private void LogIn()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[{_thisPath} / LOGIN]");
            
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            if (string.IsNullOrEmpty(username))
            {
                ShowErrorAndWaitConfirmation("Invalid username.");
                return;
            }
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                ShowErrorAndWaitConfirmation("Invalid password.");
                return;
            }

            var accountFinder = new AccountFinderService("data/accounts.txt");
            MockPassanger? account = accountFinder.GetAccountFromFile(username, password);
        
            if (!(account == null)) {
                bool isAdmin;
                if (account.AccountType == "admin") isAdmin = true;
                else isAdmin = false;
                _user = new Passenger("1234567890123", account.Username, new List<Reservation>(), isAdmin);
                return;
            }
            
            Console.Write("Invalid username or password. Would you like to try again? [Y/n]: ");
            var answer = Console.ReadLine() ?? "y";
            if (answer.ToLower() == "n") return;
        }
    }

    
    /// <summary>
    /// creates a new account
    /// </summary>
    private void SignUp()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[{_thisPath} / SIGNUP]");
            
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            if (string.IsNullOrEmpty(username)) {
                ShowErrorAndWaitConfirmation("Invalid username.");
                continue;
            }

            Console.Write("Password: ");
            string? password = Console.ReadLine();
            if (string.IsNullOrEmpty(password)) {
                ShowErrorAndWaitConfirmation("Invalid password.");
                continue;
            }

            Console.WriteLine("Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) {
                ShowErrorAndWaitConfirmation("Invalid name.");
                continue;
            }

            Console.WriteLine("CNP: ");
            string? cnp = Console.ReadLine();
            if (string.IsNullOrEmpty(cnp)) {
                ShowErrorAndWaitConfirmation("Invalid CNP.");
                continue;
            }
        
            MockPassanger newAccount = new MockPassanger(
                "user",
                username,
                password,
                cnp,
                new List<string>()
            );

            bool isAdmin;
            if (newAccount.AccountType == "admin") isAdmin = true;
            else isAdmin = false;
            _user = new Passenger("1234567890123", newAccount.Username, new List<Reservation>(), isAdmin);
            
            // Console.Write("Invalid username. Would you like to try again? [Y/n]: ");
            // var answer = Console.ReadLine() ?? "y";
            // if (answer.ToLower() == "n") return;
        }
    }
}