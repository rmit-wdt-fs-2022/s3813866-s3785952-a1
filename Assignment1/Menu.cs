using Main.Sql;
using Microsoft.Extensions.Configuration;
using Utillities;

namespace Main
{
    public class Menu
    {
        public readonly LoginMenu _loginMenu = new();

        public void MainMenu(string? name)
        {
            StoreConnectionString.GetInstance()?.SetConnectionString(GetConnectionString());
            while (true)
            {
                var menuChoices = new[] {1, 2, 3, 4, 5, 6};
                var menuOptions = new[] {"Deposit", "Withdraw", "Transfer", "My Statement", "Logout", "Exit"};
                // Console.Clear();
                Console.WriteLine($"--- {name} ---");
                for (var i = 0; i < menuChoices.Length; i++) Console.WriteLine($"[{menuChoices[i]}] {menuOptions[i]} ");
                Console.WriteLine();
                Console.WriteLine("Enter an option:");
                var userInput = Console.ReadLine();

                if (Utilities.CheckStringIsAnInt(userInput))
                {
                    Console.Clear();
                    var chosenOption = Utilities.ConvertToInt32(userInput);
                    MenuOptions(chosenOption, name);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That is not a valid option. Please Try Again");
                    Thread.Sleep(1000);
                    MainMenu(name);
                }
            }
        }

        private void MenuOptions(int option, string? name)
        {
            switch (option)
            {
                case 1:
                    Deposit();
                    break;
                case 2:
                    Withdraw();
                    break;
                case 3:
                    Transfer();
                    break;
                case 4:
                    MyStatement();
                    break;
                case 5:
                    Logout();
                    break;
                case 6:
                    Exit();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("That is not a valid option. Please Try Again");
                    Thread.Sleep(1000);
                    MainMenu(name);
                    break;
            }
        }

        private void Deposit()
        {
            var dp = new Deposit();
            dp.DepositMenu();
        }

        private void Withdraw()
        {
            var withdraw = new Withdraw();
            withdraw.WithdrawMenu();
        }

        private void Transfer()
        {
        }

        private void MyStatement()
        {
            var ms = new MyStatement();
            ms.MyStatementMenu();
        }

        private void Logout()
        {
            Console.Clear();
            Console.WriteLine("Thank you for banking with the Most Common Bank of Australia \n" +
                              "We hope to see you back!");
            Thread.Sleep(5000);
            Console.Clear();
            _loginMenu.LoginMenuDisplay(GetConnectionString());
        }

        private void Exit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for banking with the Most Common Bank of Australia \n" +
                              "We hope to see you back!");
            Utilities.WriteColoured("Program ending...", ConsoleColor.Red);
            Environment.Exit(0);
        }

        private string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("app-settings.json").Build();
            var connectionString = configuration.GetConnectionString("Database");

            return connectionString;
        }
    }
}