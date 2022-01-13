using Utillities;
namespace Main
{
    public class Menu
    {
        private readonly LoginMenu lm = new();
        private readonly Utilities util = new();

        public void MainMenu(string name)
        {
            var menuChoices = new[] {1, 2, 3, 4, 5, 6};
            var menuOptions = new[] {"Deposit", "Withdraw", "Transfer", "My Statement", "Logout", "Exit"};
            Console.Clear();
            Console.WriteLine($"--- {name} ---");
            while (true)
            {
                for (var i = 0; i < menuChoices.Length; i++) Console.WriteLine($"[{menuChoices[i]}] {menuOptions[i]} ");
                Console.WriteLine();
                Console.WriteLine("Enter an option:");
                var userInput = Console.ReadLine();

                if (Utilities.CheckStringIsAnInt(userInput))
                {
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

        private void MenuOptions(int option, string name)
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
        }

        private void Withdraw()
        {
        }

        private void Transfer()
        {
        }

        private void MyStatement()
        {
        }

        private void Logout()
        {
            Console.Clear();
            Console.WriteLine("Thank you for banking with the Most Common Bank of Australia \n" +
                              "We hope to see you back!");
            Thread.Sleep(5000);
            Console.Clear();
            lm.LoginMenuDisplay(util.GetConnectionString());
        }

        private void Exit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for banking with the Most Common Bank of Australia \n" +
                              "We hope to see you back!");
            Environment.Exit(0);
        }
    }
}