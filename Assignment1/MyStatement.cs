using Main.Model;
using Main.Sql;

namespace Main;

public class MyStatement
{
    public void MyStatementMenu()
    {
        const char NextPage = 'n';
        const char PreviousPage = 'p';
        const char Quit = 'q';
        var accountManager = new AccountManager(StoreConnectionString.GetInstance()?.GetConnectionString());
        var transactionManager = new TransactionManager(StoreConnectionString.GetInstance()?.GetConnectionString());
        var customerId = StoreCustomerDetails.GetInstance()!.GetCustomerId();

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
        var pages = Convert.ToInt32(Math.Ceiling((double) transactions.Count / 4));

        int i = 0;
        while (true)
        {
            try
            {
                if (i * 4 < transactions.Count)
                {
                    Console.WriteLine(
                        "ID   Type   AccountNumber    Destination   Amount         Time                    Comment");
                    foreach (var t in transactions.Skip(i * 4).Take(4))
                    {
                        Console.WriteLine(
                            $"{t.TransactionID,-4} {t.TransactionType,-6} {t.AccountNumber,-16} {t.DestinationAccountNumber,-14}" +
                            $"{t.Amount.ToString("C"),-14} {t.TransactionTimeUtc,-23} {t.Comment}");
                    }

                    Console.WriteLine($"Page {i} of {pages}");
                    Console.WriteLine();
                    Console.WriteLine("Options: n (next page) | p (previous page) | q (quit)");
                    Console.Write("Enter option: ");

                    var input = Console.ReadLine();
                    if (input.Length < 2)
                    {
                        var newInput = input[0];
                        if (newInput.Equals(NextPage))
                            i++;
                        else if (newInput.Equals(PreviousPage))
                            if (i == 0)
                            {
                                Console.WriteLine("Not valid input");
                            }
                            else
                            {
                                i--;
                            }
                        else if (newInput.Equals(Quit))
                            break;
                        else
                        {
                            Console.WriteLine("Not valid input");
                            Console.WriteLine();
                        }
                            
                    }
                    else
                    {
                        Console.WriteLine("Not valid input");
                        Console.WriteLine();
                    }
                }
                else
                {
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input");
            }

            Console.WriteLine();
        }
    }
}