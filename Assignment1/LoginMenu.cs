using Main.Sql;
using Utillities;

namespace Main;

public class LoginMenu
{
    public void LoginMenuDisplay(string connectionString)
    {
        while (true)
        {
            var db = new DatabaseManager(connectionString);
            Menu m = new();

            Console.WriteLine("Welcome to Most Common Bank of Australia (MCBA) \n" +
                              "To get started enter your credentials below");
            Console.Write("Enter Login ID: ");
            var loginId = Console.ReadLine();
            Console.Write("Enter Password: ");
            var password = Utilities.ReadPassword();

            var loginIdEightDigitsLong = loginId != null && Utilities.IsEightDigits(loginId) && !loginId.Equals(0);

            var passwordHashMatch = false;
            if (loginIdEightDigitsLong)
            {
                var details = db.GetLogin(Utilities.ConvertToInt32(loginId));
                passwordHashMatch = Utilities.HashVerification(details.PasswordHash, password);
            }

            var validLoginDetails = passwordHashMatch && loginIdEightDigitsLong;

            switch (validLoginDetails)
            {
                case true:
                    m.MainMenu("NAME TO DO");
                    break;
                case false:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid LoginID and / or Password. \n" +
                                      "Please Check Your Details And Try Again \n" +
                                      "You will be directed back to login in 5 seconds");
                    Console.ResetColor();
                    Thread.Sleep(5000);
                    Console.Clear();
                    continue;
            }

            break;
        }
    }
}