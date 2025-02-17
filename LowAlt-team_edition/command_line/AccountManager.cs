using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt_team_edition.command_line;

public class AccountManager : Messages
{
    private string _thisPath = "ACCOUNT MANAGEMENT";

    private readonly DataContext _dataContext;
    
    
    public AccountManager(DataContext dataContext)
    {
        _dataContext = dataContext;
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
                "   (0): Exit\n" +
                "   (1): Log in\n" +
                "   (2): Sign up"
            );
            Console.Write(">> ");
            var option = Console.ReadLine() ?? "0";

            switch (option)
            {
                case "0": return;
                case "1": LogIn(); return;
                case "2": SignUp(); return;
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
                ShowErrorAndWaitConfirmation("Invalid username!");
                return;
            }
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                ShowErrorAndWaitConfirmation("Invalid password!");
                return;
            }

            AccountLoaderService accountLoader = new AccountLoaderService(_dataContext.AccountsFile, _dataContext.Logger);
            Passenger? user = accountLoader.GetAccount(username, password, _dataContext.Flights);
        
            if (user != null) {
                _dataContext.User = user;
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
            if (string.IsNullOrWhiteSpace(username)) {
                ShowErrorAndWaitConfirmation("Invalid username!");
                continue;
            }
            bool isTaken = false;
            foreach (var account in _dataContext.Passengers) {
                if (account.Username == username) {
                    isTaken = true;
                    break;
                }
            }
            if (isTaken) {
                Console.WriteLine("This username is taken!");
                return;
            }

            Console.Write("Password: ");
            string? password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password)) {
                ShowErrorAndWaitConfirmation("Invalid password!");
                continue;
            }

            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) {
                ShowErrorAndWaitConfirmation("Invalid name!");
                continue;
            }

            Console.Write("CNP: ");
            string? cnp = Console.ReadLine()??" ";
            if ((string.IsNullOrWhiteSpace(cnp))|| (!VerificareCnpService.verificare(cnp))) {
                
                ShowErrorAndWaitConfirmation("Invalid CNP!");
                continue;
            }

            Passenger user = new Passenger(
                cnp,
                username,
                password,
                new List<Reservation>(),
                false
            );

            _dataContext.Passengers.Add(user);
            _dataContext.User = user;

            return;
            
            // Console.Write("Invalid username. Would you like to try again? [Y/n]: ");
            // var answer = Console.ReadLine() ?? "y";
            // if (answer.ToLower() == "n") return;
        }
    }
}