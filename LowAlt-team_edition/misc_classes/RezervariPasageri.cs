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
    
/// <summary>
/// afisare zboruri disponibile
/// </summary>
    public void ZboruriDisponibile()
    {
        int i = -1;
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
    public void RezervareLocuri()
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
            break;

        }


    }
}