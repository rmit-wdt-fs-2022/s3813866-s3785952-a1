using Main.Sql;

namespace Main;

public class LoginMenu
{
    public void LoginMenuDisplay(string connectionString)
    {
        var db = new DatabaseManager(connectionString);
        Menu m = new();


        Console.WriteLine("Welcome to Most Common Bank of Australia (MCBA) \n" +
                          "To get started enter your credentials below");
        Console.Write("Enter Login ID: ");
        var loginId = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Utilities.ReadPassword();

        var loginIdEightDigitsLong = loginId != null && Utilities.IsEightDigits(loginId);
        var details = db.GetLogin(Utilities.ConvertToInt32(loginId));
        var passwordHashMatch = Utilities.HashVerification(details.PasswordHash, password);

        var validLoginDetails = passwordHashMatch && loginIdEightDigitsLong;

        switch (validLoginDetails)
        {
            case true:
                m.MainMenu("NAME TO DO");
                break;
            case false:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid LoginID and / or Password. \n " +
                                  "Please Check Your Details And Try Again");
                Console.ResetColor();
                break;
        }
    }
}