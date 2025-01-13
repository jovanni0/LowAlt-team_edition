namespace LowAlt_team_edition.misc_classes;

public class RezervariPasageri
{
    private Passenger _pasager;
    private List<Flight> _listaZboruri;

    public RezervariPasageri(Passenger pasager, List<Flight> listaZboruri)
    {
        _pasager = pasager;
        _listaZboruri = listaZboruri;
    }

    public void ZboruriDisponibile()
    {
        foreach (Flight zbor in _listaZboruri)
        {
            if (zbor.AvailableSeats > 0)
            {
                Console.WriteLine(zbor.ToString());
                Console.WriteLine("\n******************************\n");
            }
        }
    }
}