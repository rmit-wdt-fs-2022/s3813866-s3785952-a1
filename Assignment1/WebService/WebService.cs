using Newtonsoft.Json;
using Main;
namespace Main.WebServiceAPI;

public static class WebServiceAPI
{
    public static void SaveCustomerInDB(string connectionString)
    {
        // // Check if any people already exist and if they do stop.
        // var personManager = new PersonManager(connectionString);
        // if(personManager.People.Any())
        //     return;
    
        const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";
    
        // Contact webservice.
        using var client = new HttpClient();
        var json = client.GetStringAsync(Url).Result;
        
        Console.WriteLine(json);
    
        // Convert JSON into objects.
        var customers = JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
        {
            // // See here for DateTime format string documentation:
            // // https://docs.microsoft.com/en-au/dotnet/standard/base-types/custom-date-and-time-format-strings
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });
        
        foreach (var person in customers)
        {
            foreach (var trans in person.accounts)
            {
                Console.WriteLine(trans.AccountNumber);
                Console.WriteLine(trans.Transactions.Count);
                foreach (var time in trans.Transactions)
                {
                    Console.WriteLine(time.TransactionTimeUtc);
                }
            }
            
        }
        
            
        // Insert into database.
        // var petManager = new PetManager(connectionString);
        // foreach(var person in people)
        // {
        //     personManager.InsertPerson(person);
        //
        //     foreach(var pet in person.Pets)
        //     {
        //         // Set pet's PersonID.
        //         pet.PersonID = person.PersonID;
        //
        //         petManager.InsertPet(pet);
        //     }
        // }
    }
}