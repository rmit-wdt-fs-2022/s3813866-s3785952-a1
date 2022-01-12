using Newtonsoft.Json;
using Main.Model;
using Main.Sql;

namespace Main.WebService;

public static class WebService
{
    public static void SaveCustomerInDb(string connectionString)
    {
        // Check if any people already exist and if they do stop.
        var DdManager = new DatabaseManager(connectionString);
        if (DdManager.Customers.Any())
            return;

        const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

        // Contact webservice.
        using var client = new HttpClient();
        var json = client.GetStringAsync(Url).Result;
        // Convert JSON into objects.
        var customers = JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
        {
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });
        
        foreach (var customer in customers)
        {
            DdManager.AddCustomer(customer);
            customer.Login.CustomerId = customer.CustomerId;
            DdManager.AddLogin(customer.Login);
            foreach (var account in customer.Accounts)
            {

                decimal totalB = 0;
                foreach (var transaction in account.Transactions)
                {
                    totalB += transaction.Amount;
                }

                account.Balance = totalB;
                DdManager.AddAccount(account);
                
                foreach (var transaction in account.Transactions)
                {
                    transaction.TransactionType = 'D';
                    transaction.AccountNumber = account.AccountNumber;
                    DdManager.AddTransaction(transaction);

                }
            }

        }

        Console.WriteLine(DdManager.GetLogin(2100).LoginId);
        Console.WriteLine(DdManager.GetTransaction(4100).First().AccountNumber);
        Console.WriteLine(DdManager.GetTransaction(4101).First().TransactionTimeUtc);
        // Console.WriteLine(DdManager.GetTransaction(4100).First().TransactionTimeUtc);
        Console.WriteLine(DdManager.GetAccounts(2100).First().Balance);
        
    }
}