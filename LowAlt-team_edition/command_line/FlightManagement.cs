namespace LowAlt_team_edition.misc_classes;

public class FlightManagement
{
    private readonly DataContext _dataContext;

    public FlightManagement(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void AdagareRuta(Ruta ruta)
    {
        _dataContext.Routes.Add(ruta);
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n*** MENIU GESTIONARE ZBORURI ***\n");
            Console.WriteLine("0. Iesire");
            Console.WriteLine("1. Adaugare zbor nou");
            Console.WriteLine("2. Stergere zbor");
            Console.WriteLine("3. Vizualizare lista completa zboruri");
            Console.WriteLine("4. Actualizare informatii zbor");
            Console.WriteLine("5. Vizualizare rute disponibile");
            Console.WriteLine("6. Adaugare ruta noua");
            Console.WriteLine("7. Stergere ruta");
            
            Console.Write("Introduceti optiunea: ");
            string optiune = Console.ReadLine() ?? " ";

            switch (optiune)
            {
                case "0":
                    return;
                    break;
                case "1":
                    AdaugareZbor();
                    break;
                case "2":
                    StergereZbor();
                    break;
                case "3":
                    VizualizareListaZboruri();
                    break;
                case "4":
                    ActualizareInformatiiZbor();
                    break;
                case "5":
                    VizualizareRuteDisponibile();
                    break;
                case "6":
                    AdaugareRutaNoua();
                    break;
                case "7":
                    StergereRuta();
                    break;
                default:
                    Console.WriteLine("Optiune invalida!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    /// <summary>
    /// Adaugare zbor in lista pentru admini
    /// </summary>
    private void AdaugareZbor()
    {
        Console.Write("\nIntroduceti ID zbor: ");
        string? idZbor = Console.ReadLine();
        
        Console.Write("Introduceti tipul zborului (1 - Local, 2 - International): ");
        string? tipZbor = Console.ReadLine();
        
        Console.Write("Introduceti ID ruta: ");
        string? idRuta = Console.ReadLine();

        if (idZbor == null || idRuta == null || tipZbor == null)
        {
            Console.WriteLine("Datele introduse sunt invalide!");
            return;
        }
        
        Console.Write("Introduceti ora plecarii: ");
        
        if (!TimeOnly.TryParse(Console.ReadLine(), out TimeOnly oraPlecare))
        {
            Console.WriteLine("Timp invalid!");
            Console.ReadLine();
            return;
        }
        
        Console.Write("Introduceti durata zborului (minute): ");

        if (!int.TryParse(Console.ReadLine(), out int durataZbor) || durataZbor <= 0)
        {
            Console.WriteLine("Durata invalida!");
            Console.ReadLine();
            return;
        }
        
        Console.Write("Introduceti numarul total de locuri: ");

        if (!int.TryParse(Console.ReadLine(), out int totalLocuri) || totalLocuri <= 0)
        {
            Console.WriteLine("Numar de locuri invalid!");
            Console.ReadLine();
            return;
        }
        
        Ruta ruta = _dataContext.Routes.Find(r => r.IDRuta == idRuta);

        if (ruta == null)
        {
            Console.WriteLine("Ruta inexistenta!");
            Console.ReadLine();
            return;
        }

        Flight zborNou;

        if (tipZbor == "1")
        {
            zborNou = new LocalFlight(idZbor, ruta, oraPlecare, durataZbor, totalLocuri, totalLocuri);
        }
        else if (tipZbor == "2")
        {
            zborNou = new InternationalFlight(idZbor, ruta, oraPlecare, durataZbor, totalLocuri, totalLocuri);
        }
        else
        {
            Console.WriteLine("Tipul zborului este invalid!");
            Console.ReadLine();
            return;
        }

        _dataContext.Flights.Add(zborNou);
        Console.WriteLine("Zborul a fost adaugat cu succes!");
        Console.ReadLine();
    }

    /// <summary>
    /// Sterge un zbor din lista adminilor
    /// </summary>
    private void StergereZbor()
    {
        Console.Write("\nIntroduceti ID-ul zborului selectat pentru stergere: ");
        string idZbor = Console.ReadLine();

        Flight zborSters = _dataContext.Flights.Find(z => z.FlightId == idZbor);

        if (zborSters != null)
        {
            _dataContext.Flights.Remove(zborSters);
            Console.WriteLine($"Zborul {idZbor} a fost sters!\n");
        }
        else
        {
            Console.WriteLine($"Zborul cu ID-ul {idZbor} nu exista!\n");
        }

        Console.ReadLine();
    }

    private void VizualizareListaZboruri()
    {
        if (_dataContext.Flights.Count == 0)
        {
            Console.WriteLine("Nu exista zboruri disponibile!");
            Console.ReadLine();
            return;
        }
        
        Console.WriteLine("\n*** LISTA ZBORURI DISPONIBILE ***");

        foreach (var zbor in _dataContext.Flights)
        {
            Console.WriteLine($"\nID Zbor: {zbor.FlightId}");
            Console.WriteLine($"Ruta: {zbor.Route.OrasPlecare} -> {zbor.Route.OrasDestinatie}");

            if (zbor is LocalFlight)
            {
                Console.WriteLine("Tipul zborului: Local");
            }
            else if (zbor is InternationalFlight)
            {
                Console.WriteLine("Tipul zborului: International");
            }
            else
            {
                Console.WriteLine("Tipul zborului: Necunoscut");
            }
            
            Console.WriteLine($"Ora plecarii: {zbor.DepartureTime}");
            Console.WriteLine($"Durata zbor: {zbor.FlightTime} min");
            Console.WriteLine($"Numar total de locuri: {zbor.Seats}");
            Console.WriteLine($"Locuri disponibile: {zbor.AvailableSeats}");

            if (zbor is LocalFlight)
            {
                Console.WriteLine($"Pret bilet: {zbor.GetSeatPrice()} RON");
            }
            else if (zbor is InternationalFlight)
            {
                Console.WriteLine($"Pret bilet: {zbor.GetSeatPrice()} RON");
            }
            else
            {
                Console.WriteLine("Pret bilet: Necunoscut");
            }
            
            Console.WriteLine("\n–––––––––––––––––––––––––");
        }

        Console.ReadLine();
    }

    /// <summary>
    /// Modifica datele unui zbor, doar adminii au acces la zona de modificari
    /// </summary>
    private void ActualizareInformatiiZbor()
    {
        Console.WriteLine("*** ACTUALIZARE INFORMATII ZBOR ***\n");
        Console.Write("\nIntroduceti ID-ul ZBORULUI pe care doriti sa-l actualizati: ");
        string idZbor = Console.ReadLine();
        
        Flight? zborActualizat = _dataContext.Flights.Find(z => z.FlightId == idZbor);

        if (zborActualizat == null)
        {
            Console.WriteLine($"Zborul {idZbor} nu exista!\n");
            Console.ReadLine();
            return;
        }
        
        Console.WriteLine($"* Detalii curente zbor {zborActualizat.FlightId} *");
        Console.WriteLine($"Ruta: {zborActualizat.Route.OrasPlecare} -> {zborActualizat.Route.OrasDestinatie}");

        if (zborActualizat is LocalFlight)
        {
            Console.WriteLine("Tipul zborului: Local");
        }
        else if (zborActualizat is InternationalFlight)
        {
            Console.WriteLine("Tipul zborului: International");
        }
        else
        {
            Console.WriteLine("Tipul zborului: Necunoscut");
        }
            
        Console.WriteLine($"Ora plecarii: {zborActualizat.DepartureTime}");
        Console.WriteLine($"Durata zbor: {zborActualizat.FlightTime} min");
        Console.WriteLine($"Numar total de locuri: {zborActualizat.Seats}");
        Console.WriteLine($"Locuri disponibile: {zborActualizat.AvailableSeats}");

        if (zborActualizat is LocalFlight)
        {
            Console.WriteLine($"Pret bilet: {zborActualizat.GetSeatPrice()} RON");
        }
        else if (zborActualizat is InternationalFlight)
        {
            Console.WriteLine($"Pret bilet: {zborActualizat.GetSeatPrice()} RON");
        }
        else
        {
            Console.WriteLine("Pret bilet: Necunoscut");
        }

        while (true)
        {
            Console.WriteLine("\n*** MENIU MODIFICARE ZBOR ***\n");
            Console.WriteLine("0. Iesire");
            Console.WriteLine("1. Ora plecare");
            Console.WriteLine("2. Durata zbor");
            Console.WriteLine("3. Numar locuri disponibile");
            Console.WriteLine("4. Modificare ruta");
            
            Console.Write("Introduceti optiunea: ");
            string optiuneActualizare = Console.ReadLine();

            switch (optiuneActualizare)
            {
                case "0":
                    return;
                case "1":
                    ModificareOraPlecare(zborActualizat);
                    return;
                case "2":
                    ModificareDurataZbor(zborActualizat);
                    return;
                case "3":
                    ModificareNumarLocuri(zborActualizat);
                    return;
                case "4":
                    ModificareRuta(zborActualizat);
                    break;
                default:
                    Console.WriteLine("Optiune invalida!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    /// <summary>
    /// Modifica ora de plecare a unui zbor
    /// </summary>
    /// <param name="zborActualizat"></param>
    private void ModificareOraPlecare(Flight zborActualizat)
    {
        Console.Write("\nIntroduceti noua ora de plecare (HH:MM): ");

        if (TimeOnly.TryParse(Console.ReadLine(), out TimeOnly oraNouaPlecare))
        {
            zborActualizat.DepartureTime = oraNouaPlecare;
            Console.WriteLine("Ora de plecare a fost actualizata!\n");
        }
        else
        {
            Console.WriteLine("Ora introdusa este invalida!");
        }

        Console.ReadLine();
    }

    private void ModificareDurataZbor(Flight zborActualizat)
    {
        Console.Write("\nIntroduceti noua durata a zborului (minute): ");

        if (int.TryParse(Console.ReadLine(), out int durataNoua) && durataNoua > 0)
        {
            zborActualizat.FlightTime = durataNoua;
            Console.WriteLine("Durata zborului a fost actualizata!\n");
        }
        else
        {
            Console.WriteLine("Durata introdusa este invalida!");
        }
        Console.ReadLine();
    }

    public void ModificareNumarLocuri(Flight zborActualizat)
    {
        Console.Write("Introduceti noul numar de locuri disponibile: ");

        if (int.TryParse(Console.ReadLine(), out int locuriNoi) && locuriNoi > 0)
        {
            zborActualizat.AvailableSeats = locuriNoi;
            Console.WriteLine("Numarul de locuri a fost actualizat!\n");
        }
        else
        {
            Console.WriteLine("Numarul de locuri introdus este invalid!");
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Modifica datele unei rute existente
    /// </summary>
    /// <param name="zborActualizat"></param>
    private void ModificareRuta(Flight zborActualizat)
    {
        Console.WriteLine("* RUTE EXISTENTE *\n");
        foreach (var ruta in _dataContext.Routes)
        {
            Console.WriteLine(ruta);
        };

        Console.Write("\nIntroduceti ID-ul rutei noi: ");
        string idRutaNoua = Console.ReadLine();

        Ruta rutaNoua = _dataContext.Routes.Find(r => r.IDRuta == idRutaNoua);

        if (rutaNoua != null)
        {
            zborActualizat.Route = rutaNoua; 
            Console.WriteLine("Ruta a fost actualizata!");
            return;
        }
        else
        {
            Console.WriteLine("Ruta introdusa nu exista!");
        }

        Console.ReadLine();
    }
    
    public void VizualizareRuteDisponibile()
    {
        Console.WriteLine("\n*** LISTA RUTELOR DISPONIBILE ***");
        
        if (_dataContext.Routes.Count == 0)
        {
            Console.WriteLine("Nu exista rute disponibile!");
        }
        else
        {
            foreach (var ruta in _dataContext.Routes)
            {
                Console.WriteLine(ruta);
            }
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Adauga o ruta noua in lista de rute
    /// </summary>
    private void AdaugareRutaNoua()
    {
        Console.Write("\nIntroduceti ID-ul rutei noi: ");
        string idRutaNoua = Console.ReadLine();
        
        Console.Write("Introduceti orasul de plecare: ");
        string orasPlecare = Console.ReadLine();
        
        Console.Write("Introduceti orasul destinatie: ");
        string orasDestinatie = Console.ReadLine();
        
        Console.Write("Introduceti numarul de km: ");

        if (!int.TryParse(Console.ReadLine(), out int numarulKm) || numarulKm <= 0)
        {
            Console.WriteLine("Numar de km invalid!");
            Console.ReadLine();
            return;
        }

        if (string.IsNullOrWhiteSpace(idRutaNoua) || string.IsNullOrWhiteSpace(orasPlecare) ||
            string.IsNullOrWhiteSpace(orasDestinatie) || string.IsNullOrWhiteSpace(orasPlecare))
        {
            Console.WriteLine("Date invalide!");
            Console.ReadLine();
            return;
        }

        if (_dataContext.Routes.Any(r => r.IDRuta == idRutaNoua))
        {
            Console.WriteLine($"Ruta cu ID-ul {idRutaNoua} exista deja!");
            Console.ReadLine();
            return;
        }

        Ruta rutaNoua = new Ruta(orasPlecare, orasDestinatie, numarulKm, idRutaNoua);
        _dataContext.Routes.Add(rutaNoua);
        
        Console.WriteLine("\nRuta a fost adaugata cu succes!");
        Console.ReadLine();
    }

    /// <summary>
    /// Sterge o ruta din lista rutelor
    /// </summary>
    private void StergereRuta()
    {
        Console.Write("\nIntroduceti ID-ul rutei pentru stergere: ");
        string idRuta = Console.ReadLine();
        
        Ruta rutaStearsa = _dataContext.Routes.FirstOrDefault(r => r.IDRuta == idRuta);

        if (rutaStearsa != null)
        {
            _dataContext.Routes.Remove(rutaStearsa);
            Console.WriteLine($"Ruta {idRuta} a fost stearsa!");
        }
        else
        {
            Console.WriteLine($"Ruta {idRuta} nu exista!");
        }

        Console.ReadLine();
    }
}