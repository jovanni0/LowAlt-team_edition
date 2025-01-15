namespace LowAlt_team_edition.services;

public class RouteWriterService
{
    private string _filePath;


    public RouteWriterService(string filePath)
    {
        _filePath = filePath;
    }


    public void WriteRoutesToFile(List<Ruta> routes)
    {
        string database = GenerateDatabase(routes);
        if (!File.Exists(_filePath)) {
            try {
                File.Create(_filePath).Dispose();
            }
            catch (Exception e) {
                Console.WriteLine($"Could not create file {_filePath}: {e}");
                return;
            }
        }
        try {
            File.WriteAllText(_filePath, database);
        }
        catch (Exception e) {
            Console.WriteLine($"Could not write to file {_filePath}: {e}");
        }
    }


    private string GenerateDatabase(List<Ruta> routes)
    {
        string header = "PLECARE-SOSIRE KM ID\n" +
                        "--------------------";
        foreach (var route in routes) {
            string entry = StringifyFlight(route);
            header += $"\n{entry}";
        }

        return header;
    }


    private string StringifyFlight(Ruta route) 
    {
        return $"{route.OrasPlecare} {route.OrasDestinatie} {route.Km} {route.IDRuta}";
    }
}
