using System.Net;
using System.Reflection.Metadata;

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

    public void  InteractiunePasageri()
    {
        while (true)
        {
            Console.Clear();
            string optiune;
            if (_pasager.IsLoggedIn == false)
            {
                Console.WriteLine("Buna ziua! Se pare ca nu sunteti conectat la un cont.");
                Console.WriteLine("Optiuni disponibile:\n(1): Parasire program\n(2): Vizualizare lista zboruri disponibile");
                Console.WriteLine("Selectati optiunea dorita: ");
                optiune = Console.ReadLine() ?? " ";

                switch (optiune)
                {
                    case "1": return;
                    case "2":ZboruriDisponibile();
                        Console.ReadLine();
                        break;
                    default: Console.WriteLine("Optiunea introdusa nu exista!\nIncercati sa introduceti o alta optiune.");
                        Console.ReadLine();
                        continue;
                    
                }
                
                
            }
            else
            {
                Console.WriteLine($"Buna ziua, {_pasager.Username}");
                Console.WriteLine("Optiuni disponibile:\n(1): Parasire program\n(2): Vizualize lista zboruri disponibile\n(3): Rezervare locuri zbor\n(4): Anulare rezervare\n(5): Vizualizare istoric rezervari proprii");
                Console.WriteLine("Selectati optiunea dorita: ");
                optiune = Console.ReadLine() ?? " ";

                switch (optiune)
                {
                    case "1": return;
                    case "2": ZboruriDisponibile();
                        Console.ReadLine();
                        break;
                    case "3": RezervareLocuri();
                        Console.ReadLine();
                        break;
                    case "4": StergereRezervare();
                        Console.ReadLine();
                        break;
                    case "5": AfisareRezervari();
                        Console.ReadLine();
                        break;
                    default: Console.WriteLine("Ne pare rau, dar optiunea introdusa nu exista!\nIncercati sa introduceti o alta optiune.");
                        Console.ReadLine();
                        continue;
                }
            }
        }
    }
    
    
    
/// <summary>
/// afisare zboruri disponibile
/// </summary>
    private void ZboruriDisponibile()
    {
        
        Console.Clear();
        int i = -1;
        Console.WriteLine("Zborurile disponibile sunt: ");
        foreach (Flight zbor in _listaZboruri)
        {
            i++;
            if (zbor.AvailableSeats > 0)
            {
                Console.Write($"({i}) ");
                Console.WriteLine(zbor.ToString());
                Console.WriteLine("\n******************************\n");
            }
        }
    }

/// <summary>
/// Rezervare locuri pentru un zbor
/// </summary>
    private void RezervareLocuri()
    {
        while (true)
        {
            int nrZborIntreg = -1;
            int numarLocuriInt = -1;

            Console.Clear();
            ZboruriDisponibile();
            Console.WriteLine("Va rugam sa alegeti unul dintre zborurile disponibile: ");
            string nrZbor = Console.ReadLine() ?? " ";
            try
            {
                nrZborIntreg = Int32.Parse(nrZbor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                continue;
            }

            if (nrZborIntreg < 0 || nrZborIntreg >= _listaZboruri.Count || _listaZboruri[nrZborIntreg].AvailableSeats == 0)
            {
                Console.WriteLine("Ne pare rau, dar zborul ales de dumneavoastra nu este disponibil momentan!");
                Console.WriteLine("Incercati sa alegeti alt zbor.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("Va rugam sa introduceti numarul de locuri dorite: ");
            string numarLocuri = Console.ReadLine() ?? " ";
            try
            {
                numarLocuriInt = Int32.Parse(numarLocuri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                continue;
            }

            if (numarLocuriInt <= 0)
            {
                Console.WriteLine("Ne pare rau, dar ati introdus un numar gresit de locuri!");
                Console.ReadLine();
                continue;
            }
            if (_listaZboruri[nrZborIntreg].AvailableSeats < numarLocuriInt)
            {
                Console.WriteLine("Ne pare rau, dar numarul introdus depaseste numarul de locuri disponibile ramase.");
                Console.ReadLine();
                continue;
            }
            
            _pasager.AddReservation(new Reservation(_pasager,_listaZboruri[nrZborIntreg], numarLocuriInt));
            _listaZboruri[nrZborIntreg].AddReservation(new Reservation(_pasager,_listaZboruri[nrZborIntreg], numarLocuriInt));
            _listaZboruri[nrZborIntreg].AvailableSeats -= numarLocuriInt;
            Console.WriteLine("Rezervarea a fost creata cu succes!");
            Console.WriteLine($"Total plata: {new Reservation(_pasager,_listaZboruri[nrZborIntreg], numarLocuriInt).Price} RON");
            break;

        }


    }

/// <summary>
/// Afisarea rezervarilor anterioare ale utilizatorului
/// </summary>
    private void AfisareRezervari()
    {
        int i = -1;
        
        Console.Clear();
        if (_pasager.PriorReservations.Count == 0)
        {
            Console.WriteLine("Lista dumneavoastra de rezervari este goala!");
        }
        else
        {
            Console.WriteLine("Lista dumneavoastra de rezervari este: ");
            foreach (Reservation rezervare in _pasager.PriorReservations)
            {
                i++;
                Console.Write($"({i}) ");
                Console.WriteLine(rezervare.ToString());
            }
        }
       
    }

/// <summary>
/// Stergerea unei rezervari facute anterior
/// </summary>
    private void StergereRezervare()
    {
        while (true)
        {
            int numarRezervareInt = -1;
            
            Console.Clear();
            Console.WriteLine("Lista dumneavoastra de rezervari este: ");
            AfisareRezervari();

            if (_pasager.PriorReservations.Count == 0)
            {
                break;
            }

            Console.WriteLine("Introduceti rezervarea la care doriti sa renuntati: ");
            string numarRezervare = Console.ReadLine() ?? " ";
            try
            {
                numarRezervareInt = Int32.Parse(numarRezervare);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                continue;
            }

            if (numarRezervareInt < 0 || numarRezervareInt >= _pasager.PriorReservations.Count)
            {
                Console.WriteLine("Ne pare rau, dar rezervarea aleasa nu exista in lista!");
                Console.WriteLine("Incercati sa alegeti o alta rezervare.");
                Console.ReadLine();
                continue;
            }

            _pasager.PriorReservations[numarRezervareInt].TargetFlight.AvailableSeats +=
                _pasager.PriorReservations[numarRezervareInt].Seats;
            _pasager.PriorReservations.RemoveAt(numarRezervareInt);
            Console.WriteLine("Rezervarea a fost eliminata cu succes!");
            break;

        }

    }
}