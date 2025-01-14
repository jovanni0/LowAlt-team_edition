using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt.UnitTests;

[TestClass]
public class AccountFinderTests
{
    [TestMethod]
    public void TryParseAccount_CorrectData_True()
    {
        AccountFinderService accountFinderService = new AccountFinderService("somepath");
        MockPassanger passanger;

        var success = accountFinderService.TryParseAccount("admin admin admin 1234567890123 01-10", out passanger);

        Assert.IsTrue(success);
    }

    [TestMethod]
    public void TryParseAccount_IncorectData_False()
    {
        AccountFinderService accountFinderService = new AccountFinderService("somepath");
        MockPassanger passanger;

        var success = accountFinderService.TryParseAccount(" admin 1234567890123", out passanger);

        Assert.IsFalse(success);
    }
}