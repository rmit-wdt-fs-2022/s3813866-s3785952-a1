using System.Text.RegularExpressions;
using Utillities;
namespace Main;

public abstract class LoginMenu
{
    public static void LoginMenuDisplay()
    {
        var rgx = new Regex("^[0-9]{8}$");

        Console.WriteLine("Welcome to Most Common Bank of Australia (MCBA) \n" +
                          "To get started enter your credentials below");
        Console.Write("Enter Login ID: ");
        var loginId = Console.ReadLine();

        var loginIdEightDigitsLong = rgx.IsMatch(loginId);

        Console.Write("Enter Password: ");
        var password = Utilities.ReadPassword();
        var passwordEightDigitsLong = rgx.IsMatch(password);


        // logic to check with sql server if they match or not
        // or pass into another class Utilities loginId and password get
        // password hash from db from loginId and if they match return true 
        // else return false 

        // within this method also pass in loginIdEightDigitsLong and passwordEightDigitsLong 
        // return true / false based on request
        const bool validLoginDetails = true;

        switch (validLoginDetails)
        {
            case true:
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