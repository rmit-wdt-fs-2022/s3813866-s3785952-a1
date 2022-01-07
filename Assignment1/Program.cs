using Microsoft.Extensions.Configuration;
using Main.Sql;

namespace Main;
public class MainEntry
    {
        private static void Main()
        {
            Console.WriteLine("here");
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            var connectionString = configuration.GetConnectionString("Main");
            Console.WriteLine(connectionString);
            

            DatabaseConnection.CreateTables(connectionString);
            
            Console.WriteLine("done");
        }
    }
