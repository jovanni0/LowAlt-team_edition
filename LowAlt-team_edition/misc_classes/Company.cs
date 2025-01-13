namespace LowAlt_team_edition.misc_classes;

public class Company
{
    /// <summary>
    /// the company name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// the fleet of the company
    /// </summary>
    public List<Flight> Fleet { get; init; }

    /// <summary>
    /// the routes if the company
    /// </summary>
    public List<Route> Routes { get; set; }

    public Company(string name, List<Flight> fleet, List<Route> routes)
    {
        Name = name;
        Fleet = fleet;
        Routes = routes;
    }
}