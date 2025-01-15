namespace LowAlt_team_edition.misc_classes;

public class InternationalFlight : Flight
{
    public InternationalFlight(string flightId, Ruta route, TimeOnly departureTime, int flightTime, int seats,
        int availableSeats) : base(flightId, route, departureTime, flightTime, seats, availableSeats)
    {
        
    }

    public InternationalFlight(MockFlight mockFlight, Ruta route) : base(mockFlight, route)
    {
        
    }

    public override double GetSeatPrice()
    {
        return 200 + Route.Km;
    }

    public override string ToString()
    {
        return $"*** International ***\n {base.ToString()}";
    }
}