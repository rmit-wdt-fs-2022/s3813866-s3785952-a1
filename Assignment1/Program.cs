using Microsoft.Extensions.Configuration;
using Main.Sql;
namespace Main;
public class MainEntry
    {
        private static void Main()
        {
            // Console.WriteLine("here");
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            //database connection test
            var connectionString = configuration.GetConnectionString("Main");
            Console.WriteLine(connectionString);
            DatabaseConnection.CreateTables(connectionString);
            WebService.WebService.SaveCustomerInDb(connectionString);


            //Hashing verification test
            // string hash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2";
            // string pw = "abc123";
            //
            // if (HashVerification.PasswordCheck(hash, pw))
            // {
            //     Console.WriteLine("input correct");
            // }
            // else
            // {
            //     Console.WriteLine("wrong input");
            // }
            
            Console.WriteLine("done");
        }
    }
