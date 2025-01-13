using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.misc_classes;

public class Reservation
{
    /// <summary>
    /// the owner of the reservations
    /// </summary>
    public Passenger Owner { get; init; }

    /// <summary>
    /// the flight containing the reserved seats
    /// </summary>
    public Flight TargetFlight { get; init; }

    /// <summary>
    /// the number of seats reserved
    /// </summary>
    public int Seats { get; init; }

    /// <summary>
    /// the price of the reservation
    /// </summary>
    public double Price { get; init; }

    public Reservation(Passenger owner, Flight targetFlight, int seats)
    {
        Owner = owner;
        TargetFlight = targetFlight;
        Seats = seats;

        Price = TargetFlight.GetSeatPrice() * Seats;
    }

    public override string ToString()
    {
        return Owner.Username + " " + TargetFlight + " " + Seats + " seats";
    }

}
