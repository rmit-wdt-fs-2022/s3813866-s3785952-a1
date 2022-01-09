using Microsoft.Data.SqlClient;
using Main.Model;
namespace Main.Sql;

public class DatabaseSeeding
{
    private readonly string _connectionString;
    
    public DatabaseSeeding(string connectionString)
    {
        _connectionString = connectionString;

        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";

        

        People = command.GetDataTable().Select().Select(x => new Person
        {
            PersonID = x.Field<int>("PersonID"),
            // OR
            // PersonID = x.Field<int>(nameof(Person.PersonID)),
            // OR
            // PersonID = (int) x["PersonID"],
            FirstName = x.Field<string>("FirstName"),
            // OR
            // FirstName = (string) x["FirstName"],
            LastName = x.Field<string>("LastName"),
            Pets = petManager.GetPets(x.Field<int>("PersonID"))
        }).ToList();
    }
    
    public void AddCustomer(Customer customer, string connectionString)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        

        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Customer (CustomerID, Name, Address, City, Postcode) values (@personID, @firstName, @lastName)";
        command.Parameters.AddWithValue("personID", Customer.);
        command.Parameters.AddWithValue("firstName", person.FirstName);
        command.Parameters.AddWithValue("lastName", person.LastName);
        command.Parameters.AddWithValue("lastName", person.LastName);
        command.Parameters.AddWithValue("lastName", person.LastName);

        command.ExecuteNonQuery();
    }
}