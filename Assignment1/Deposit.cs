using Main.Sql;
using Utillities;

namespace Main;

public class Deposit
{
    public void DepositMenu()
    {
        try
        {
            var accountManager =
                new AccountManager(StoreConnectionString.GetInstance().GetConnectionString());
            var transactionManager =
                new TransactionManager(StoreConnectionString.GetInstance().GetConnectionString());
            var facade = new Facade(StoreConnectionString.GetInstance().GetConnectionString());

            var accounts = accountManager.GetAccounts(StoreCustomerId.GetInstance().GetCustomerId());

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

            var singleAccount = facade.GetUserAccounts(StoreCustomerId.GetInstance().GetCustomerId(),
                Convert.ToInt32(selectedAccountNumber));

            if (singleAccount == null) throw new ArgumentException("Has To Be A Number");

            var accountNumberMatch = selectedAccountNumber != null &&
                                     singleAccount.AccountNumber.ToString().Equals(selectedAccountNumber);


            Console.WriteLine(
                $"You have selected account number {selectedAccountNumber} with a balance of ${singleAccount.Balance?.ToString("0.00")} and a available balance of ${singleAccount.Balance}");

            Console.Write("Enter the amount you wish to deposit: ");
            var amountToDeposit = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter comment for this deposit (n to quit, max length is 30): ");
            var comment = Console.ReadLine();

            if (Utilities.IsLong(comment) || comment.Equals("n"))
                throw new ArgumentException("Max Lenght of 30 has been reached!");

            facade.Deposit(selectedAccountNumber, (int) amountToDeposit, comment,
                Convert.ToDecimal(singleAccount.Balance?.ToString("0.00")) + (decimal) amountToDeposit);

            Console.WriteLine(
                $"We have deposited ${amountToDeposit} to account number {selectedAccountNumber} successfully.");
            Console.WriteLine("Funds should appear within the account soon.");
            Console.WriteLine(
                $"Your account balance is now ${Convert.ToDecimal(singleAccount.Balance?.ToString("0.00")) + (decimal) amountToDeposit}");
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Please Try Again As You Have Miss Typed A Word");
            Thread.Sleep(2000);
            DepositMenu();
        }
    }
}