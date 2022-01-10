using Microsoft.Extensions.Configuration;
using Main.Sql;
namespace Main;
public class MainEntry
    {
        private static void Main()
        {
             Console.WriteLine("here");
             var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
             //database connection test
             var connectionString = configuration.GetConnectionString("Main");
             //Console.WriteLine(connectionString);
             WebServiceAPI.WebServiceAPI.SaveCustomerInDb(connectionString);
             DatabaseConnection.CreateTables(connectionString);
            
             //Hashing verification test
             string hash1 = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2";
             string hash2 = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04";
             string hash3 = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE";
             string password1 = "abc123";
             string password2 = "ilovermit2020";
             string password3 = "youWill_n0tGuess-This!";
             
             Console.WriteLine($"hash 1 and p1 {Utilities.HashVerification(hash1, password1)}");
             Console.WriteLine($"hash 2 and p2 {Utilities.HashVerification(hash2, password2)}");
             Console.WriteLine($"hash 3 and p3 {Utilities.HashVerification(hash3, password3)}");
            
             /*if (Utilities.HashVerification(hash, pw))
             {
                 //Move to menu? 
                 Console.WriteLine("Move on to menu ?");
             }
             else
             {
                 //try again ?
                 Console.WriteLine("try again");
             }*/
            
            Console.WriteLine("done");
        }
    }
