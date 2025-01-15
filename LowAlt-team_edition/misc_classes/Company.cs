using LowAlt_team_edition.command_line;
using LowAlt_team_edition.services;

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
    public List<Ruta> Routes { get; set; }

    public readonly DataContext Context;

    public Company(string name, DataContext dataContext)
    {
        Name = name;
        Context = dataContext;
        Fleet = dataContext.Flights;
        Routes = dataContext.Routes;
    }

    public void Begin()
    {
        /// run the console application
        var interfata = new UserInterface(Context);
        interfata.StartInteraction();
    }
}