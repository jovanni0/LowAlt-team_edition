using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition;

public class RapoarteSiStatistici
{
    private List<Flight> _zboruri;
    private List<Passenger> _pasageri;

    public RapoarteSiStatistici(List<Flight> zboruri, List<Passenger> pasageri)
    {
        this._zboruri = zboruri;
        this._pasageri = pasageri;
    }

    /// <summary>
    /// Vizualizarea zborurilor cu cele mai multe locuri rezervate
    /// </summary>
    private void ZboruriRezervate()
    {
        Console.Clear();
        _zboruri.Sort((x,y)=>y.LocuriRezervate().CompareTo(x.LocuriRezervate()));
        Console.WriteLine("Zborurile in ordinea rezervarilor sunt:\n");
        foreach (Flight zbor in _zboruri)
        {
            Console.WriteLine(zbor.ToString());
            Console.WriteLine($"Locuri rezervate: {zbor.LocuriRezervate()}");
            Console.WriteLine("\n**************************************");
        }
    }
    
    /// <summary>
    /// Afisarea zborurilor din lista
    /// </summary>
    private void ListaZboruri()
    {
        
        Console.Clear();
        int i = -1;
        Console.WriteLine("Zborurile sunt: ");
        foreach (Flight zbor in _zboruri)
        {
            i++; 
            Console.Write($"({i}) ");
            Console.WriteLine(zbor.ToString());
            Console.WriteLine("\n******************************\n");
            
        }
    }
    
    /// <summary>
    /// Afisarea veniturilor generate de un zbor
    /// </summary>
    private void VenituriZbor()
    {
        while (true)
        {
            int numarZborInt = -1;
            ListaZboruri();
            Console.WriteLine("Selectati zborul pentru care doriti sa vizualizati veniturile:");
            string numarZbor = Console.ReadLine()??" ";
            try
            {
                numarZborInt = Int32.Parse(numarZbor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                continue;
            }
            
            if(numarZborInt < 0|| numarZborInt >= _zboruri.Count)
            {
                Console.WriteLine("Zborul selectat nu exista!\nIncercati sa introduceti un alt zbor.");
                Console.ReadLine();
                continue;
            }
            
            Console.WriteLine($"Venitul generat de zborul selectat este de {_zboruri[numarZborInt].GetRevenue()} RON");
            break;
        }
    }

    /// <summary>
    /// Afisarea raportului zilnic al veniturilor totale
    /// </summary>
    public void RaportZilnic()
    {
        Console.Clear();
        double venituriTotale = 0;
        foreach (Flight zbor in _zboruri)
        {
            venituriTotale += zbor.GetRevenue();
        }
        Console.WriteLine($"Venituri totale sunt de: {venituriTotale} RON");
    }
}