using System;

namespace LowAlt_team_edition.misc_classes;

public record MockFlight(
    string flightType,
    string flightId,
    string routeId,
    TimeOnly departureTime,
    int flightTime,
    int seats,
    int availableSeats
);