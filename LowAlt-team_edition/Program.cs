using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition;

class Program
{
    static void Main(string[] args)
    {
        DataContext dataContext = new DataContext{
            AccountsFile = "data/accounts.txt",
            FlightsFile = "data/flights.txt",
            RoutesFile = "data/routes.txt"
        };

        Company company = new Company("MurderInc", dataContext);
        company.Begin();
    }
}