using Main.Sql;
using Utillities;

namespace Main;

public class Deposit
{
    public void DepositMenu()
    {
        try
        {
            var connectionString = StoreConnectionString.GetInstance().GetConnectionString();
            var accountManager =
                new AccountManager(connectionString);
            var transactionManager =
                new TransactionManager(connectionString);
            var facade = new Facade(connectionString);
            var menu = new Menu();

            var accounts = accountManager.GetAccounts(StoreCustomerDetails.GetInstance().GetCustomerId());

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


            Console.Write("Please select the account from the list above you wish to deposit to : ");
            var selectedAccountNumber = Convert.ToInt32(Console.ReadLine());

            var singleAccount = facade.GetUserAccounts(StoreCustomerDetails.GetInstance().GetCustomerId(),
                Convert.ToInt32(selectedAccountNumber));

            if (singleAccount == null) throw new ArgumentException("Has To Be A Number");

            Console.WriteLine(
                $"You have selected account number {selectedAccountNumber} with a balance of ${singleAccount.Balance?.ToString("0.00")} and a available balance of ${singleAccount.Balance?.ToString("0.00")}");

            Console.Write("Enter the amount you wish to deposit: ");
            var amountToDeposit = Convert.ToDouble(Console.ReadLine());

            if (decimal.Round((decimal) amountToDeposit, 2) != (decimal) amountToDeposit)
                throw new ArgumentException("More than two decimals were entered!");

            Console.Write("Enter comment for this deposit (n to quit, max length is 30): ");
            var comment = Console.ReadLine();

            if (comment is "n") menu.MainMenu(StoreCustomerDetails.GetInstance()?.GetCustomerName());

            if (Utilities.IsLong(comment) || comment.Equals("n"))
                throw new ArgumentException("Max Lenght of 30 has been reached!");

            facade.Deposit(selectedAccountNumber, (int) amountToDeposit, comment,
                Convert.ToDecimal(singleAccount.Balance?.ToString("0.00")) + (decimal) amountToDeposit);

            Console.WriteLine(
                $"We have deposited ${amountToDeposit.ToString("0.00")} to account number {selectedAccountNumber} successfully.");
            Console.WriteLine("Funds should appear within the account soon.");

            var totalDeposited = Convert.ToDecimal(singleAccount.Balance) +
                                 Convert.ToDecimal(amountToDeposit);
            Console.WriteLine(
                $"Your account balance is now ${totalDeposited.ToString("0.00")}");
            Thread.Sleep(3000);
            Console.Clear();
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Please Try Again As You Have Miss Typed A Word");
            Thread.Sleep(3000);
            Console.Clear();
            DepositMenu();
        }
    }
}