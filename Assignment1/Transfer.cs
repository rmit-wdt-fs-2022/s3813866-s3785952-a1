using Main.Sql;
using Utillities;

namespace Main;

public class Transfer
{
    public void TransferMenu()
    {
        Console.Clear();
        var connectionString = StoreConnectionString.GetInstance()?.GetConnectionString();
        var accountManager =
            new AccountManager(connectionString);
        var transactionManager =
            new TransactionManager(connectionString);
        var facade = new Facade(connectionString);
        var menu = new Menu();
        var accounts = accountManager.GetAccounts(StoreCustomerDetails.GetInstance()!.GetCustomerId());
        

        Console.WriteLine(@"  _______                   __            __  __                  
 |__   __|                 / _|          |  \/  |                 
    | |_ __ __ _ ___ _ __ | |_ ___ _ __  | \  / | ___ _ __  _   _ 
    | | '__/ _` / __| '_ \|  _/ _ \ '__| | |\/| |/ _ \ '_ \| | | |
    | | | | (_| \__ \ | | | ||  __/ |    | |  | |  __/ | | | |_| |
    |_|_|  \__,_|___/_| |_|_| \___|_|    |_|  |_|\___|_| |_|\__,_|");

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
            "Please select the destination account number from the list above you wish to deposit to , or enter 'c' to cancel and go back to menu : ");
        var selectedAccountNumber = Console.ReadLine();

        if (selectedAccountNumber != null && selectedAccountNumber.ToLower().Equals("c"))
        {
            Console.Clear();
            Console.WriteLine("Transferring you back to the main menu one second");
            Thread.Sleep(2000);
            Console.Clear();
            menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());
        }

        if (Utilities.CheckStringIsAnInt(selectedAccountNumber))
        {
            var convertToIntSelectedAccountNumber = Convert.ToInt32(selectedAccountNumber);
            try
            {
                var singleAccount = facade.GetUserAccounts(StoreCustomerDetails.GetInstance()!.GetCustomerId(),
                    convertToIntSelectedAccountNumber);

                if (singleAccount == null) throw new ArgumentException("No Account Found");


                var savings = 'S';
                if (singleAccount.AccountType.Equals(savings))
                    Console.WriteLine(
                        $"You have selected account number {convertToIntSelectedAccountNumber} with a balance of ${singleAccount.Balance?.ToString("0.00")} and a available balance of ${singleAccount.Balance?.ToString("0.00")}");

                var checking = 'C';
                if (singleAccount.AccountType.Equals(checking))
                    Console.WriteLine(
                        $"You have selected account number {convertToIntSelectedAccountNumber} with a balance of ${singleAccount.Balance?.ToString("0.00")} and a available balance of ${(singleAccount.Balance - 300)?.ToString("0.00")}");

                
                Console.Write("Enter the account number you wish to transfer: ");
                var destinationAccountInput = Console.ReadLine();
                var destinationAccount = Convert.ToInt32(destinationAccountInput);

                
                if (checkAccount(accountManager.CheckTable(), destinationAccount))
                {
                    Console.WriteLine($"You have chosen the account: {destinationAccount}");
                }
                else
                {
                    throw new ArgumentException("Invalid account number");
                }
                
                Console.Write(
                    "Enter the amount you wish to transfer to, or enter 'c' to cancel and go back to menu : ");
                var amountToTransfer = Console.ReadLine();

                var convertedAmountToTransfer = Convert.ToInt32(amountToTransfer);

                if (amountToTransfer.ToLower().Equals("c"))
                {
                    Console.Clear();
                    Console.WriteLine("Transferring you back to the main menu one second");
                    Thread.Sleep(2000);
                    Console.Clear();
                    menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());
                }

                if (convertedAmountToTransfer < 0)
                {
                    Console.WriteLine("You cannot withdraw negative numbers");
                    Thread.Sleep(2000);
                    return;
                }

                if (convertedAmountToTransfer > singleAccount.Balance - 300)
                {
                    Console.WriteLine("Amount cannot be greater than available balance");
                    Thread.Sleep(2000);
                }

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

                if (decimal.Round(convertedAmountToTransfer, 2) != convertedAmountToTransfer)
                    throw new ArgumentException("You Cannot Enter more than two decimal places");

                var numberOfTransactions = facade.NumberOfTransactions(convertToIntSelectedAccountNumber);

                var transferFee = 0.10m;

                var receieverAccountBalance = accountManager.GetAccountByAccNum(destinationAccount);
                decimal senderBalance = Convert.ToDecimal(singleAccount.Balance) - convertedAmountToTransfer;
                decimal receiverBalance = Convert.ToDecimal(receieverAccountBalance.Balance) + convertedAmountToTransfer;
                
                    
                if (numberOfTransactions < 2)
                    facade.Transfer(convertToIntSelectedAccountNumber,destinationAccount ,convertedAmountToTransfer, comment,
                        senderBalance, receiverBalance);
                else
                    facade.TransferWithFee(convertToIntSelectedAccountNumber, destinationAccount, convertedAmountToTransfer, comment,
                        senderBalance,receiverBalance ,transferFee);

                Console.WriteLine(
                    $"We have withdrew ${convertedAmountToTransfer} to account number {convertToIntSelectedAccountNumber} successfully.");
                Console.WriteLine("Funds should disappear within the account soon.");
                
                Console.WriteLine(
                    $"Your account balance is now ${senderBalance:0.00}");
                Thread.Sleep(3000);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
                Thread.Sleep(3000);
                Console.Clear();
                TransferMenu();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
            Thread.Sleep(3000);
            Console.Clear();
            TransferMenu();
        }
        
    }

    private bool checkAccount(List<Account> accounts, int destinationAccount)
    {
        foreach (var account in accounts)
        {
            if (account.AccountNumber == destinationAccount)
            {
                return true;
            }
        }
        return false;
    }
}