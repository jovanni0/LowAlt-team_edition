using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition;

public class RapoarteSiStatistici
{
    private readonly DataContext _dataContext;

    public RapoarteSiStatistici(DataContext dataContext)
    {
       this._dataContext = dataContext;
    }

    /// <summary>
    /// Interactiune cu un administrator
    /// </summary>
    public void InteractiuneAdministrator()
    {
        while (true)
        {
            Console.Clear();
            string optiune;
            Console.WriteLine("\nBine ati revenit!");
            Console.WriteLine("(0): Parasire program\n(1): Vizualizarea zborurilor cu cele mai multe locuri rezervate\n(2): Vizualizarea veniturilor generate de un zbor\n(3): Generare raport zilnic al veniturilor totale\n(4): Vizualizarea platilor efectuate de un pasager");
            Console.Write("Selectati optiunea dorita: ");
            optiune = Console.ReadLine()??" ";

            switch (optiune)
            {
                case "0": 
                    return;
                case "1": 
                    ZboruriRezervate();
                    Console.ReadLine();
                    break;
                case "2": 
                    VenituriZbor();
                    Console.ReadLine();
                    break;
                case "3": 
                    RaportZilnic();
                    Console.ReadLine();
                    break;
                case "4": 
                    PlatiPasager();
                    Console.ReadLine();
                    break;
                default: 
                    Console.WriteLine("Ne pare rau, dar optiunea selectata nu exista!");
                    Console.WriteLine("Incercati sa introduceti o alta optiune.");
                    Console.ReadLine();
                    continue;
            }
        }
    }

    /// <summary>
    /// Vizualizarea zborurilor cu cele mai multe locuri rezervate
    /// </summary>
    private void ZboruriRezervate()
    {
        Console.Clear();
        //Expresie LINQ pentru sortarea descrescatoare a zborurilor in 
        //functie de numarul de locuri rezervate
        
        var sortedFlights = _dataContext.Flights
            .OrderByDescending(f => f.LocuriRezervate())
            .ToList();
        Console.WriteLine("Zborurile in ordinea rezervarilor sunt:\n");
        foreach (Flight zbor in sortedFlights)
        {
            Console.WriteLine(zbor.ToString());
            Console.WriteLine($"Locuri rezervate: {zbor.LocuriRezervate()}");
            Console.WriteLine("–––––––––––––––––––––––––");
        }
    }
    
    /// <summary>
    /// Afisarea zborurilor din lista
    /// </summary>
    private void ListaZboruri()
    {
        
        Console.Clear();
        int i = -1;
        Console.WriteLine("\nZborurile sunt: ");
        foreach (Flight zbor in _dataContext.Flights)
        {
            i++; 
            Console.Write($"({i}) ");
            Console.WriteLine(zbor.ToString());
            Console.WriteLine("–––––––––––––––––––––––––");
            
        }
    }
    
    /// <summary>
    /// Afisarea veniturilor generate de un zbor
    /// </summary>
    private void VenituriZbor()
    {
        while (true)
        {
            Console.Clear();
            int numarZborInt = -1;
            ListaZboruri();
            Console.Write("Selectati zborul pentru care doriti sa vizualizati veniturile: ");
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
            
            if(numarZborInt < 0 || numarZborInt >= _dataContext.Flights.Count)
            {
                Console.WriteLine("\nZborul selectat nu exista!\nIncercati sa introduceti un alt zbor.");
                Console.ReadLine();
                continue;
            }
            
            Console.WriteLine($"\nVenitul generat de zborul selectat este de {_dataContext.Flights[numarZborInt].GetRevenue()} RON");
            break;
        }
    }

    /// <summary>
    /// Afisarea raportului zilnic al veniturilor totale
    /// </summary>
    private void RaportZilnic()
    {
        Console.Clear();
        //Metoda LINQ pt calcularea veniturilor totale (aduna veniturile din fiecare zbor 
        //si salveaza totalul in variabila venituriTotale)
        double venituriTotale = _dataContext.Flights.Sum(zbor => zbor.GetRevenue());
        Console.WriteLine($"Venituri totale sunt de: {venituriTotale} RON");
    }

    private void ListaPasageri()
    {
        Console.Clear();
        int i = -1;
        Console.WriteLine("\nLista de pasageri este: ");
        foreach (Passenger pasager in _dataContext.Passengers)
        {
            i++;
            Console.Write($"({i}) ");
            Console.WriteLine(pasager.ToString());
            Console.WriteLine("–––––––––––––––––––––––––");
        }
        
    }

    /// <summary>
    /// Vizualizarea tuturor platilor efectuate de un pasager
    /// </summary>
    private void PlatiPasager()
    {
        while (true)
        {
            Console.Clear();
            int numarPasagerInt = -1;
            ListaPasageri();
            if (_dataContext.Passengers.Count == 0)
            {
                Console.WriteLine("\nNe pare rau, dar momentan lista de pasageri este goala!");
                break;
            }
            Console.Write("\nSelectati pasagerul dorit: ");
            string numarPasager = Console.ReadLine()??" ";
            try
            {
                numarPasagerInt = Int32.Parse(numarPasager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                continue;
            }

            if (numarPasagerInt < 0 || numarPasagerInt >= _dataContext.Passengers.Count)
            {
                Console.WriteLine("\nPasagerul selectat nu exista in lista!\nIncercati sa selectati un alt pasager.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("\nPlatile efectuate de pasagerul selectat sunt:\n");
            int plata = 0;
            foreach (Reservation rezervare in _dataContext.Passengers[numarPasagerInt].PriorReservations)
            {
                plata++;
                Console.WriteLine($"Plata numarul {plata}: {rezervare.TargetFlight} - {rezervare.Price} RON");
                Console.WriteLine("–––––––––––––––––––––––––");
            }
            break;
        }
    }
}