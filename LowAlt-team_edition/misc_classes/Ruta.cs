namespace LowAlt_team_edition;

public class Ruta
{
    public string OrasPlecare  { get; set; }
    public string OrasDestinatie { get; set; }
    public int Km { get; set; }
    public string IDRuta { get; set; }

    public Ruta(string orasPlecare, string orasDestinatie, int km, string idRuta)
    {
        OrasPlecare = orasPlecare;
        OrasDestinatie = orasDestinatie;
        Km = km;
        IDRuta = idRuta;
    }

    public override string ToString()
    {
        return $"ID Ruta: {IDRuta} | {OrasPlecare} -> {OrasDestinatie} ({Km} km)";
    }
}