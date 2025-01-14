using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt.UnitTests;

[TestClass]
public class FlightReaderTests
{
    [TestMethod]
    public void TryParseFlight_CorrectEntry_True()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 553 129", 
            out flight
        );

        Assert.IsTrue(success);
    }

    [TestMethod]
    public void TryParseFlight_InvalidType_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "unknown RO101 01 10:20 121 553 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_InvalidDepartureTime_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 25:02 121 553 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_OutOfBoundsFlightTime_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 -5 553 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_InvalidFormatFlightTime_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 3dp 553 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_OutOfBoundsSeats_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 0 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_InvalidFormatSeats_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 hhds- 129", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_OutOfBoundsAvailableSeats_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 553 -1", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_ZeroSeats_True()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 553 0", 
            out flight
        );

        Assert.IsTrue(success);
    }

    [TestMethod]
    public void TryParseFlight_InvalidFormatAvailableSeats_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 553 ppp", 
            out flight
        );

        Assert.IsFalse(success);
    }

    [TestMethod]
    public void TryParseFlight_MoreFieleds_True()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121 553 138 kjsbkj asdjkb893 83874", 
            out flight
        );

        Assert.IsTrue(success);
    }

    [TestMethod]
    public void TryParseFlight_NotEnoughFieleds_False()
    {
        FlightReaderService flightReaderService = new FlightReaderService("somepath");
        MockFlight flight;

        var success = flightReaderService.TryParseFlight(
            "local RO101 01 10:20 121", 
            out flight
        );

        Assert.IsFalse(success);
    }
}