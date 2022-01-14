using Main.Sql;
using Utillities;

namespace Main;

public class Deposit
{
    public void DepositMenu()
    {
        Console.Clear();
        var connectionString = StoreConnectionString.GetInstance().GetConnectionString();
        var accountManager =
            new AccountManager(connectionString);
        var transactionManager =
            new TransactionManager(connectionString);
        var facade = new Facade(connectionString);
        var menu = new Menu();

        var accounts = accountManager.GetAccounts(StoreCustomerDetails.GetInstance().GetCustomerId());

        Console.WriteLine(@"  _____                       _ _     __  __                  
 |  __ \                     (_) |   |  \/  |                 
 | |  | | ___ _ __   ___  ___ _| |_  | \  / | ___ _ __  _   _ 
 | |  | |/ _ \ '_ \ / _ \/ __| | __| | |\/| |/ _ \ '_ \| | | |
 | |__| |  __/ |_) | (_) \__ \ | |_  | |  | |  __/ | | | |_| |
 |_____/ \___| .__/ \___/|___/_|\__| |_|  |_|\___|_| |_|\__,_|
             | |                                              
             |_|                                              ");

        Console.WriteLine("---------------------------------------------------\n" +
                          "| ACCOUNT TYPE | ACCOUNT NUMBER | ACCOUNT BALANCE |\n" +
                          "---------------------------------------------------");

        foreach (var account in accounts)
        {
            Console.Write("  " + (account.AccountType == 'S' ? "Savings" : "Checking") + "\t ");
            Console.Write(account.AccountNumber + "\t");
            Console.WriteLine("\t  $" + account.Balance?.ToString("0.00"));
        }

        Console.WriteLine();


        Console.Write(
            "Please select the account from the list above you wish to deposit to , or enter 'c' to cancel and go back to menu : ");
        var selectedAccountNumber = Console.ReadLine();

        if (selectedAccountNumber != null && selectedAccountNumber.ToLower().Equals("c"))
        {
            Console.Clear();
            return;
        }

        if (Utilities.CheckStringIsAnInt(selectedAccountNumber))
        {
            var convertToIntSelectedAccountNumber = Convert.ToInt32(selectedAccountNumber);

            try
            {
                var singleAccount = facade.GetUserAccounts(StoreCustomerDetails.GetInstance()!.GetCustomerId(),
                    convertToIntSelectedAccountNumber);

                if (singleAccount == null) throw new ArgumentException("No Account Found");

                Console.WriteLine(
                    $"You have selected account number {selectedAccountNumber} with a balance of ${singleAccount.Balance?.ToString("0.00")} and a available balance of ${singleAccount.Balance?.ToString("0.00")}");

                Console.Write("Enter the amount you wish to deposit, or enter 'c' to cancel and go back to menu : ");
                var amountToDeposit = Console.ReadLine();

                if (amountToDeposit != null && amountToDeposit.ToLower().Equals("c"))
                {
                    Console.Clear();
                    Console.WriteLine("Transferring you back to the main menu one second");
                    Thread.Sleep(2000);
                    Console.Clear();
                    menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());
                }

                var convertedAmountToDeposit = Convert.ToDecimal(amountToDeposit);

                if (decimal.Round(convertedAmountToDeposit, 2) != convertedAmountToDeposit)
                    throw new ArgumentException("You Cannot Enter more than two decimal places");

                Console.Write(
                    "Enter comment for this deposit (max length is 30) , or enter 'c' to cancel and go back to menu : ");
                var comment = Console.ReadLine();

                if (comment != null && (Utilities.IsLong(comment) || comment.Equals("c")))
                {
                    Console.Clear();
                    Console.WriteLine("Transferring you back to the main menu one second");
                    Thread.Sleep(2000);
                    Console.Clear();
                    menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());
                }


                facade.Deposit(convertToIntSelectedAccountNumber, convertedAmountToDeposit, comment,
                    Convert.ToDecimal(singleAccount.Balance?.ToString("0.00")) + convertedAmountToDeposit);

                Console.WriteLine(
                    $"We have deposited ${convertedAmountToDeposit} to account number {convertToIntSelectedAccountNumber} successfully.");
                Console.WriteLine("Funds should appear within the account soon.");

                var totalDeposited = Convert.ToDecimal(singleAccount.Balance) +
                                     Convert.ToDecimal(convertedAmountToDeposit);
                Console.WriteLine(
                    $"Your account balance is now ${totalDeposited:0.00}");
                Thread.Sleep(3000);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
                Thread.Sleep(3000);
                Console.Clear();
                DepositMenu();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
            Thread.Sleep(3000);
            Console.Clear();
            DepositMenu();
        }
    }
}