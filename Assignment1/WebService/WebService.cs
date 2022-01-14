using Newtonsoft.Json;
using Main.Model;
using Main.Sql;

namespace Main.WebService;

public static class WebService
{
    public static void SaveCustomerInDb(string connectionString)
    {
        // Check if any people already exist and if they do stop.
        var loginManager = new LoginManager(connectionString);
        var customerManager = new CustomerManager(connectionString);
        var accountManager = new AccountManager(connectionString);
        var transactionManager = new TransactionManager(connectionString);

        if (customerManager.CheckTable().Any() || transactionManager.CheckTable().Any())
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
            customerManager.Add(customer);
            customer.Login.CustomerId = customer.CustomerId;
            loginManager.Add(customer.Login);
            foreach (var account in customer.Accounts)
            {
        
                decimal totalB = 0;
                foreach (var transaction in account.Transactions)
                {
                    totalB += transaction.Amount;
                }
        
                account.Balance = totalB;
                accountManager.Add(account);
                
                foreach (var transaction in account.Transactions)
                {
                    transaction.TransactionType = 'D';
                    transaction.AccountNumber = account.AccountNumber;
                    transactionManager.Add(transaction);
        
                }
            }
        
        }
        
        
    }
}