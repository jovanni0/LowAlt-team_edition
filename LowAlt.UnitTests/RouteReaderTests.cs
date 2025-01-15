using LowAlt_team_edition;
using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt.UnitTests;

[TestClass]
public class RouteReaderTests
{
    [TestMethod]
    public void TryParseRoute_CorrectEntry_True()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;
        Ruta expectedRoute = new Ruta("Timisoara", "Arad", 87, "01");

        var success = routeReaderService.TryParseRoute(
            "Timisoara-Arad 87 01", 
            out route
        );

        Assert.IsTrue(success);
        Assert.IsTrue(expectedRoute.Equals(route));
    }

    [TestMethod]
    public void TryParseRoute_InvalidLocation_False()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;

        var success = routeReaderService.TryParseRoute(
            "TimisoaraArad 87 01", 
            out route
        );

        Assert.IsFalse(success);
        Assert.IsTrue(route == null);
    }

    [TestMethod]
    public void TryParseRoute_OutOfBoundsKilometers_False()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;

        var success = routeReaderService.TryParseRoute(
            "Timisoara-Arad -87 01", 
            out route
        );

        Assert.IsFalse(success);
        Assert.IsTrue(route == null);
    }

    [TestMethod]
    public void TryParseRoute_ZeroKilometers_False()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;

        var success = routeReaderService.TryParseRoute(
            "Timisoara-Arad 0 01", 
            out route
        );

        Assert.IsFalse(success);
        Assert.IsTrue(route == null);
    }

    [TestMethod]
    public void TryParseRoute_NotEnoughParameters_False()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;

        var success = routeReaderService.TryParseRoute(
            "Timisoara-Arad 0", 
            out route
        );

        Assert.IsFalse(success);
        Assert.IsTrue(route == null);
    }

    [TestMethod]
    public void TryParseRoute_LotsOfParameters_True()
    {
        RouteReaderService routeReaderService = new RouteReaderService("somepath");
        Ruta route;
        Ruta expectedRoute = new Ruta("Timisoara", "Arad", 100, "01");

        var success = routeReaderService.TryParseRoute(
            "Timisoara-Arad 100 01 skjbc 838y ia8dc", 
            out route
        );

        Assert.IsTrue(success);
        Assert.IsTrue(route.Equals(expectedRoute));
    }
}