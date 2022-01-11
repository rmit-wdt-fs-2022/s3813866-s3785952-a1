namespace Main
{
    public class Menu
    {
        public static void MainMenu(string name)
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

        public static void MenuOptions(int option, string name)
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

        public static void Deposit()
        {
        }

        public static void Withdraw()
        {
        }

        public static void Transfer()
        {
        }

        public static void MyStatement()
        {
        }

        public static void Logout()
        {
        }

        public static void Exit()
        {
        }
    }
}