namespace LowAlt_team_edition.misc_classes;

public class Passenger
{
    /// <summary>
    /// cnp of the passenger
    /// </summary>
    public string Cnp { get; init; }

    /// <summary>
    /// list of reservations in the past
    /// </summary>
    public List<Reservation> PriorReservations { get; init; }

    /// <summary>
    /// username of the passenger
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// indicates if the passenger is admin
    /// </summary>
    public bool IsAdmin { get; init; }

    /// <summary>
    /// indicates if the pasaenger is logged in
    /// </summary>
    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Username);
    
    public Passenger(string cnp, string username, List<Reservation> priorReservations, bool isAdmin)
    {
        Cnp = cnp;
        Username = username;
        PriorReservations = priorReservations;
        IsAdmin = isAdmin;
    }

    /// <summary>
    /// Adaugare rezervare 
    /// </summary>
    public void AddReservation(Reservation reservation)
    {
        PriorReservations.Add(reservation);
    }
    public override string ToString()
    {
        string toReturn = $"Pasager {Username}, Cnp: {Cnp}:";
        foreach (var reservation in PriorReservations) {
            toReturn += $"\n -+ {reservation.TargetFlight.Route.OrasPlecare} -> {reservation.TargetFlight.Route.OrasDestinatie} ({reservation.TargetFlight.Route.Km} km)" +
                        $", Ora plecare: {reservation.TargetFlight.DepartureTime}";
        }
        return toReturn;
    }
}