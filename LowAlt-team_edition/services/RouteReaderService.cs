using LowAlt_team_edition.misc_classes;

namespace LowAlt_team_edition.services;

public class RouteReaderService : Messages
{
    private string _pathToFile;

    public RouteReaderService(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    public List<Ruta> GetRoutes()
    {
        List<Ruta> rute = new List<Ruta>();

        using (StreamReader file = new StreamReader(_pathToFile)) {
            file.ReadLine(); file.ReadLine(); // skip first 2 lines

            string? line;
            while ((line = file.ReadLine()) != null) {
                string[] parts = line.Split(" ");

                if (parts.Length < 3) {
                    ShowError($"Invalid route entry: {line}");
                    continue;
                }
                
                int distance;
                try
                {
                    distance = int.Parse(parts[1]);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"distance argument null for route: {line}");
                    continue;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"distance argument format error for route: {line}");
                    continue;
                }

                string[] locations = parts[0].Split("-");
                if (locations.Length < 2) {
                    ShowError($"Invalid start and end locations (start-end): {line}");
                    continue;
                }

                Ruta ruta = new Ruta(locations[0], locations[1], distance, parts[2]);
                rute.Add(ruta);
            }
        }

        return rute;
    }
}