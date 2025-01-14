namespace LowAlt_team_edition.misc_classes;

public class FlightManagement
{
    private List<Flight> lista_zboruri;
    private List<Ruta> lista_rute;

    public FlightManagement()
    {
        lista_zboruri = new List<Flight>();
        lista_rute = new List<Ruta>();
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
            string optiune = Console.ReadLine();

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
                    
                    break;
                case "4":
                    
                    break;
                case "5":
                    VizualizareRuteDisponibile();
                    break;
                case "6":
                    
                    break;
                case "7":
                    
                    break;
                default:
                    Console.WriteLine("Optiune invalida! (Apasa ENTER pentru a reveni la meniu)");
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
            Console.WriteLine("Timp invalid! (Apasa ENTER pentru a reveni la meniu)");
            Console.ReadLine();
            return;
        }
        
        Console.Write("Introduceti durata zborului (minute): ");

        if (!int.TryParse(Console.ReadLine(), out int durataZbor) || durataZbor <= 0)
        {
            Console.WriteLine("Durata invalida! (Apasa ENTER pentru a reveni la meniu)");
            Console.ReadLine();
            return;
        }
        
        Console.Write("Introduceti numarul total de locuri: ");

        if (!int.TryParse(Console.ReadLine(), out int totalLocuri) || totalLocuri <= 0)
        {
            Console.WriteLine("Numar de locuri invalid! (Apasa ENTER pentru a reveni la meniu)");
            Console.ReadLine();
            return;
        }
        
        Ruta ruta = lista_rute.Find(r => r.IDRuta == idRuta);

        if (ruta == null)
        {
            Console.WriteLine("Ruta inexistenta! (Apasa ENTER pentru a reveni la meniu)");
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
            Console.WriteLine("Tipul zborului este invalid! (Apasa ENTER pentru a reveni la meniu)");
            Console.ReadLine();
            return;
        }

        lista_zboruri.Add(zborNou);
        Console.WriteLine("Zborul a fost adaugat cu succes! (Apasa ENTER pentru a reveni la meniu)");
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
            Console.WriteLine($"Zborul {idZbor} a fost sters! (Apasa ENTER pentru a reveni la meniu)\n");
        }
        else
        {
            Console.WriteLine($"Zborul cu ID-ul {idZbor} nu exista! (Apasa ENTER pentru a reveni la meniu)\n");
        }

        Console.ReadLine();
    }
    
    
    
    
    private void VizualizareRuteDisponibile()
    {
        foreach (var ruta in lista_rute)
        {
            Console.WriteLine(ruta);
        }
    }
    
}