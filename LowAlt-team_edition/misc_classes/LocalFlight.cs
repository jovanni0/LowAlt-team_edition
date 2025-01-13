namespace LowAlt_team_edition.misc_classes;

public class LocalFlight : Flight
{
    public LocalFlight(string flightId, Ruta route, TimeOnly departureTime, int flightTime, int seats,
        int availableSeats) : base(flightId, route, departureTime, flightTime, seats, availableSeats)
    {
        
    }

    public override double GetSeatPrice()
    {
        return 50 + (0.5 * Route.Km);
    }
}