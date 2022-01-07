using Newtonsoft.Json;
namespace Main.WebService;

public class WebService
{
    // public static void GetAndSavePeople(string connectionString)
    // {
    //     // Check if any people already exist and if they do stop.
    //     var personManager = new PersonManager(connectionString);
    //     if(personManager.People.Any())
    //         return;
    //
    //     const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/example/";
    //
    //     // Contact webservice.
    //     using var client = new HttpClient();
    //     var json = client.GetStringAsync(Url).Result;
    //
    //     // Convert JSON into objects.
    //     var people = JsonConvert.DeserializeObject<List<Person>>(json, new JsonSerializerSettings
    //     {
    //         // See here for DateTime format string documentation:
    //         // https://docs.microsoft.com/en-au/dotnet/standard/base-types/custom-date-and-time-format-strings
    //         DateFormatString = "dd/MM/yyyy"
    //     });
    //         
    //     // Insert into database.
    //     var petManager = new PetManager(connectionString);
    //     foreach(var person in people)
    //     {
    //         personManager.InsertPerson(person);
    //
    //         foreach(var pet in person.Pets)
    //         {
    //             // Set pet's PersonID.
    //             pet.PersonID = person.PersonID;
    //
    //             petManager.InsertPet(pet);
    //         }
    //     }
    // }
}