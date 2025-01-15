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

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(Ruta)) return false;

        return ((Ruta)obj).OrasPlecare == this.OrasPlecare && 
               ((Ruta)obj).OrasDestinatie == this.OrasDestinatie &&
               ((Ruta)obj).Km == this.Km &&
               ((Ruta)obj).IDRuta == this.IDRuta;
    }
}