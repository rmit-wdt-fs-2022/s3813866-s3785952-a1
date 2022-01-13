using System.Text.Json;
using Newtonsoft.Json;
using Main.Model;
using Main.Sql;
using JsonException = Newtonsoft.Json.JsonException;


namespace Main.WebService;

public class WebService
{
    private const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

    
    public static async Task SaveCustomerInDbAsync(string connectionString)
    {
        try
        {
            // Check if any people already exist and if they do stop.
            var DbManager = new DatabaseManager(connectionString);
            if (DbManager.Customers.Any())
                return;


            // Contact webservice.
            using var client = new HttpClient();
            var json = await client.GetStringAsync(Url);

            Console.WriteLine("here");
            Console.WriteLine(json);
            // Convert JSON into objects.
            var customers = JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
            }) ?? throw new JsonException("Json Object Deserialize error");

            foreach (var customer in customers)
            {
                DbManager.AddCustomer(customer);
                customer.Login.CustomerId = customer.CustomerId;
                DbManager.AddLogin(customer.Login);
                foreach (var account in customer.Accounts)
                {
                    decimal totalB = 0;
                    foreach (var transaction in account.Transactions)
                    {
                        totalB += transaction.Amount;
                    }

                    account.Balance = totalB;
                    DbManager.AddAccount(account);

                    foreach (var transaction in account.Transactions)
                    {
                        transaction.TransactionType = 'D';
                        transaction.AccountNumber = account.AccountNumber;
                        DbManager.AddTransaction(transaction);

                    }
                }

            }

            Console.WriteLine(DbManager.GetLogin(17963428).PasswordHash);
            Console.WriteLine(DbManager.GetTransaction(4100).First().AccountNumber);
            Console.WriteLine(DbManager.GetTransaction(4101).First().TransactionTimeUtc);
            // Console.WriteLine(DdManager.GetTransaction(4100).First().TransactionTimeUtc);
            Console.WriteLine(DbManager.GetAccounts(2100).First().Balance);

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}