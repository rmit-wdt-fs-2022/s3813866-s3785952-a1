using Utillities;

namespace Main.Sql;

public class Withdraw
{
    public void WithdrawMenu()
    {
        // TODO

        Console.Clear();
        var connectionString = StoreConnectionString.GetInstance().GetConnectionString();
        var accountManager =
            new AccountManager(connectionString);
        var transactionManager =
            new TransactionManager(connectionString);
        var facade = new Facade(connectionString);
        var menu = new Menu();
        var accounts = accountManager.GetAccounts(StoreCustomerDetails.GetInstance().GetCustomerId());

        Console.WriteLine(@"
 __          ___ _   _         _                      __  __                  
 \ \        / (_) | | |       | |                    |  \/  |                 
  \ \  /\  / / _| |_| |__   __| |_ __ __ ___      __ | \  / | ___ _ __  _   _ 
   \ \/  \/ / | | __| '_ \ / _` | '__/ _` \ \ /\ / / | |\/| |/ _ \ '_ \| | | |
    \  /\  /  | | |_| | | | (_| | | | (_| |\ V  V /  | |  | |  __/ | | | |_| |
     \/  \/   |_|\__|_| |_|\__,_|_|  \__,_| \_/\_/   |_|  |_|\___|_| |_|\__,_|");

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

                Console.Write(
                    "Enter the amount you wish to withdraw, or enter 'c' to cancel and go back to menu : ");
                var amountToWithdraw = Console.ReadLine();

                var convertedAmountToWithdraw = Convert.ToInt32(amountToWithdraw);

                if (amountToWithdraw.ToLower().Equals("c"))
                {
                    Console.Clear();
                    Console.WriteLine("Transferring you back to the main menu one second");
                    Thread.Sleep(2000);
                    Console.Clear();
                    menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());
                }

                if (convertedAmountToWithdraw < 0)
                {
                    Console.WriteLine("You cannot withdraw negative numbers");
                    Thread.Sleep(2000);
                    return;
                }

                if (convertedAmountToWithdraw > singleAccount.Balance - 300)
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

                if (decimal.Round(convertedAmountToWithdraw, 2) != convertedAmountToWithdraw)
                    throw new ArgumentException("You Cannot Enter more than two decimal places");

                var numberOfTransactions = facade.NumberOfTransactions(convertToIntSelectedAccountNumber);

                decimal withdrawFee = (decimal)0.05;
                decimal balance = Convert.ToDecimal(singleAccount.Balance) - Convert.ToDecimal(amountToWithdraw);

                if (numberOfTransactions < 2)
                    facade.Withdraw(convertToIntSelectedAccountNumber, convertedAmountToWithdraw, comment,
                        balance);
                else
                    facade.WithdrawWithFee(convertToIntSelectedAccountNumber, convertedAmountToWithdraw, comment,
                        balance, withdrawFee);

                Console.WriteLine(
                    $"We have withdrew ${convertedAmountToWithdraw} to account number {convertToIntSelectedAccountNumber} successfully.");
                Console.WriteLine("Funds should disappear within the account soon.");

                
                Console.WriteLine(
                    $"Your account balance is now ${balance:0.00}");
                Thread.Sleep(3000);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
                Thread.Sleep(3000);
                Console.Clear();
                WithdrawMenu();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Please Try Again Input Provided Is Not Correct!");
            Thread.Sleep(3000);
            Console.Clear();
            WithdrawMenu();
        }


        // var accountSelected = Console.ReadLine();

        // same as deposit 


        // somehow keep track of the service fee
        // my opinion do a sql query
        // SELECT TransactionType.Count() FROM Transaction WHERE TRANSACTIONTYPE = 'W || TRANSACTIONTYPE = "T' AND accountnumber = acocuntnumber
        // this should give 0  if they have done no withdraw 
        // make a method all i give you is acocuntnumber u give me back an int 

        // when we withdraw i will pass you accountNumeberSelected, amountToBeDeposited and comment and how many withdraws
        // they have made based on this number you will do the service fee charge

        // last method is give me the balance of the current account i give you accountNumber 
    }
}