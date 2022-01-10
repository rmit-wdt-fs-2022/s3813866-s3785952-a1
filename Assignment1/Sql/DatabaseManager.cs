using System.Data;
using Microsoft.Data.SqlClient;
using Main.Model;
namespace Main.Sql;

public class DatabaseManager
{
    private readonly string _connectionString;
    
    public List<Customer> Customers { get; }
    
    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;

        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";
        
        Customers = command.GetDataTable().Select().Select(x => new Customer
        {
            CustomerId = x.Field<int>("CustomerID"),
            // OR
            // PersonID = x.Field<int>(nameof(Person.PersonID)),
            // OR
            // PersonID = (int) x["PersonID"],
            Name = x.Field<string>("Name"),
            // OR
            // FirstName = (string) x["FirstName"],
            Address = x.Field<string>("LastName"),
            City = x.Field<string>("LastName")
        }).ToList();
    }
    
    public void AddCustomer(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Customer (CustomerID, Name, Address, City, Postcode) values (@CustomerID, @Name, @Address, @City, @Postcode)";
        command.Parameters.AddWithValue("CustomerID", customer.CustomerId);
        command.Parameters.AddWithValue("Name", customer.Name);
        command.Parameters.AddWithValue("Address", customer.Address);
        command.Parameters.AddWithValue("City", customer.City);
        command.Parameters.AddWithValue("Postcode", customer.PostCode);

        command.ExecuteNonQuery();
    }

    public void AddAccount(Account account)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Account (AccountNumber, AccountType, CustomerId) values (@AccountNumber, @AccountType, @CustomerId)";
        command.Parameters.AddWithValue("AccountNumber", account.AccountNumber);
        command.Parameters.AddWithValue("AccountType", account.AccountType);
        command.Parameters.AddWithValue("CustomerId", account.CustomerId);

        command.ExecuteNonQuery();
    }

    public void AddTransaction(Transaction transaction)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Account (Amount, Comment, transactionTimeUtc) values (@Amount, @Comment, @transactionTimeUtc)";
        command.Parameters.AddWithValue("personID", transaction.Amount);
        command.Parameters.AddWithValue("firstName", transaction.Comment);
        command.Parameters.AddWithValue("lastName", transaction.TransactionTimeUtc);

        command.ExecuteNonQuery();
    }

    public void AddLogin(Login login)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Account (LoginID, PasswordHash) values (@Amount, @Comment)";
        command.Parameters.AddWithValue("personID", login.LoginId);
        command.Parameters.AddWithValue("firstName", login.PasswordHash);
        
        command.ExecuteNonQuery();
    }
}