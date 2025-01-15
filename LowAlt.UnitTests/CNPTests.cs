using LowAlt_team_edition;

namespace LowAlt.UnitTests;


[TestClass]
public class CNPTests
{
    [TestMethod]
    public void verificare_CNPcorect_True()
    {
        bool rezultat = VerificareCnpService.verificare("5102236291203");
        
        Assert.IsTrue(rezultat);
    }

    [TestMethod]
    public void verificare_CNPlitere_False()
    {
        bool rezultat = VerificareCnpService.verificare("729sd92js3812");
        
        Assert.IsFalse(rezultat);
    }

    [TestMethod]
    public void verificare_CNPscurt_False()
    {
        bool rezultat = VerificareCnpService.verificare("82931138");
        
        Assert.IsFalse(rezultat);
    }

    [TestMethod]
    public void verificare_CNPlung_False()
    {
        bool rezultat = VerificareCnpService.verificare("291293201929122");
        
        Assert.IsFalse(rezultat);
    }

    [TestMethod]
    public void verificare_CNPscurtCuLitere_False()
    {
        bool rezultat = VerificareCnpService.verificare("31asdj2");
        
        Assert.IsFalse(rezultat);
    }

    [TestMethod]
    public void verificare_CNPlungCuLitere_False()
    {
        bool rezultat = VerificareCnpService.verificare("6192hsja912819wjs2");
        
        Assert.IsFalse(rezultat);
    }
}