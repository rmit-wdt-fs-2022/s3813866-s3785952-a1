using Main.Sql;

namespace Main;

public class MyStatement
{
    public void MyStatementMenu()
    {
        var accountManager = new AccountManager(StoreConnectionString.GetInstance()?.GetConnectionString());
        var transactionManager = new TransactionManager(StoreConnectionString.GetInstance()?.GetConnectionString());
        var customerId = StoreCustomerId.GetInstance()!.GetCustomerId();

        const string statementMenuBanner = @"____ ___ ____ ___ ____ _  _ ____ _  _ ___    _  _ ____ _  _ _  _ 
[__   |  |__|  |  |___ |\/| |___ |\ |  |     |\/| |___ |\ | |  | 
___]  |  |  |  |  |___ |  | |___ | \|  |     |  | |___ | \| |__|";
        Console.WriteLine(statementMenuBanner);
        Console.WriteLine("---------------------------------------------------\n" +
                          "| ACCOUNT TYPE | ACCOUNT NUMBER | ACCOUNT BALANCE |\n" +
                          "---------------------------------------------------");
        var accounts = accountManager.GetAccounts(customerId);
        foreach (var account in accounts)
        {
            Console.Write("  " + (account.AccountType == 'S' ? "Savings" : "Checking") + "\t ");
            Console.Write(account.AccountNumber + "\t");
            Console.WriteLine("\t  $" + account.Balance?.ToString("0.00"));
        }

        Console.WriteLine();
        Console.WriteLine("Please select the account from the list above you wish to view : ");
        var selectedAccountNumber = Console.ReadLine();

        var transactions = transactionManager.GetTransaction(Convert.ToInt32(selectedAccountNumber));


        foreach (var t in transactions)
        {
            Console.WriteLine(t.TransactionID);
            Console.WriteLine(t.TransactionType);
            Console.WriteLine(t.AccountNumber);
            Console.WriteLine(t.DestinationAccountNumber);
            Console.WriteLine(t.Amount);
            Console.WriteLine(t.Comment);
            Console.WriteLine(t.TransactionTimeUtc);
        }
    }
}