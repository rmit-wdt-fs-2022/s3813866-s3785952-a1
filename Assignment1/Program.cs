using Main.Sql;
using Microsoft.Extensions.Configuration;
using Utillities;

namespace Main;

public class Program
{
    private static async Task Main()
    {
        
        var configuration = new ConfigurationBuilder().AddJsonFile("app-settings.json").Build();

        //database connection test
        var connectionString = configuration.GetConnectionString("Database");
        DatabaseConnection.CreateTables(connectionString);
        WebService.WebService.SaveCustomerInDb(connectionString);

        var facade = new Facade(connectionString);
        // facade.Deposit(4100, 20, "hello", 10);
        // facade.Transfer(4101,4200,30,"something3",100);
        facade.Transfer(4101,4300,309,"zhong xina",1036);
        facade.Transfer(4101,4200,300,"yi long musk",100);
        facade.Transfer(4101,4200,300,"sum ting wong",2070);
        facade.Withdraw(4101, 300, "withdraw", 1000);


        Console.WriteLine("done");

        var p = new Program();
        p.Run(connectionString);
    }

    public void Run(string? connectionString)
    {
        Utilities.Disclaimer();
        var lm = new LoginMenu();
        lm.LoginMenuDisplay(connectionString);
    }
}