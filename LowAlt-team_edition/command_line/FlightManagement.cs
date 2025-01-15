namespace LowAlt_team_edition.misc_classes;

public class FlightManagement
{
    private List<Flight> lista_zboruri;
    private List<Ruta> lista_rute;

    public FlightManagement(List<Flight> zboruri, List<Ruta> rute)
    {
        lista_zboruri = zboruri;
        lista_rute = rute;
    }

    public void AdagareRuta(Ruta ruta)
    {
        lista_rute.Add(ruta);
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
                    
                    break;
                case "7":
                    
                    break;
                default:
                    Console.WriteLine("Optiune invalida!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void AdaugareZbor()
    {
        Console.Write("\nIntroduceti ID zbor: ");
        string idZbor = Console.ReadLine();
        
        Console.Write("Introduceti tipul zborului (1 - Local, 2 - International): ");
        string tipZbor = Console.ReadLine();
        
        Console.Write("Introduceti ID ruta: ");
        string idRuta = Console.ReadLine();
        
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
        
        Ruta ruta = lista_rute.Find(r => r.IDRuta == idRuta);

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

        lista_zboruri.Add(zborNou);
        Console.WriteLine("Zborul a fost adaugat cu succes!");
        Console.ReadLine();
    }

    private void StergereZbor()
    {
        Console.Write("\nIntroduceti ID-ul zborului selectat pentru stergere: ");
        string idZbor = Console.ReadLine();

        Flight zborSters = lista_zboruri.Find(z => z.FlightId == idZbor);

        if (zborSters != null)
        {
            lista_zboruri.Remove(zborSters);
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
        if (lista_zboruri.Count == 0)
        {
            Console.WriteLine("Nu exista zboruri disponibile!");
            Console.ReadLine();
            return;
        }
        
        Console.WriteLine("\n*** LISTA ZBORURI DISPONIBILE ***");

        foreach (var zbor in lista_zboruri)
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
            
            Console.WriteLine("\n--------------------------------------------");
        }

        Console.ReadLine();
    }

    private void ActualizareInformatiiZbor()
    {
        Console.WriteLine("*** ACTUALIZARE INFORMATII ZBOR ***\n");
        Console.Write("\nIntroduceti ID-ul ZBORULUI pe care doriti sa-l actualizati: ");
        string idZbor = Console.ReadLine();
        
        Flight? zborActualizat = lista_zboruri.Find(z => z.FlightId == idZbor);

        if (zborActualizat == null)
        {
            Console.WriteLine($"Zborul {idZbor} nu exista!\n");
            Console.ReadLine();
            return;
        }
        
        Console.WriteLine($"-- Detalii curente zbor {zborActualizat.FlightId} --");
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

    private void ModificareOraPlecare(Flight zborActualizat)
    {
        Console.Write("\nIntroduceti noua ora de plecare: ");

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

    private void ModificareRuta(Flight zborActualizat)
    {
        Console.WriteLine("-- RUTE EXISTENTE --\n");
        foreach (var ruta in lista_rute)
        {
            Console.WriteLine(ruta);
        };

        Console.Write("\nIntroduceti ID-ul rutei noi: ");
        string idRutaNoua = Console.ReadLine();

        Ruta rutaNoua = lista_rute.Find(r => r.IDRuta == idRutaNoua);

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
        
        if (lista_rute.Count == 0)
        {
            Console.WriteLine("Nu exista rute disponibile.");
        }
        else
        {
            foreach (var ruta in lista_rute)
            {
                Console.WriteLine(ruta);
            }
        }
        Console.ReadLine();
    }
}