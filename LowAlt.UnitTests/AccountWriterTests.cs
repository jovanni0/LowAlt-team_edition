using LowAlt_team_edition.misc_classes;
using LowAlt_team_edition.services;

namespace LowAlt.UnitTests;

[TestClass]
public class AccountWriterTests
{
    [TestMethod]
    public void StringifyPassenger_DateCorecte_True()
    {
        AccountWriterService test1 = new AccountWriterService(" ", new MockLogger());
        MockPassanger passanger1 = new MockPassanger(
            "admin", 
            "user123", 
            "parola1", 
            "1234567891234",
            new List<string> { "id1-34", "id2-0" });
        string dataCorect="admin user123 parola1 1234567891234 id1-34 id2-0";
        
        string data = test1.StringifyPassenger(passanger1);
        
        Assert.IsTrue(dataCorect == data);
    }
}