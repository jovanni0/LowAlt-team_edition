using System;

namespace LowAlt_team_edition.misc_classes;

public record MockPassanger(
    string AccountType,
    string Username,
    string Password,
    string Cnp,
    List<string> Reservations
);