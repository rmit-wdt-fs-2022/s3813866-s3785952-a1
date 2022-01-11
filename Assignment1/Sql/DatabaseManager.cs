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
            Name = x.Field<string>("Name"),
            Address = x.Field<string>("Address"),
            City = x.Field<string>("City")
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
        command.Parameters.AddWithValue("Address",customer.Address != null ? customer.Address : DBNull.Value);
        command.Parameters.AddWithValue("City", customer.City != null ? customer.City : DBNull.Value);
        command.Parameters.AddWithValue("Postcode", customer.PostCode!= null ? customer.PostCode : DBNull.Value);

        command.ExecuteNonQuery();
    }

    public void AddAccount(Account account)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Account (AccountNumber, AccountType, CustomerId, Balance) values (@AccountNumber, @AccountType, @CustomerId, @Balance)";
        command.Parameters.AddWithValue("AccountNumber", account.AccountNumber);
        command.Parameters.AddWithValue("AccountType", account.AccountType);
        command.Parameters.AddWithValue("CustomerId", account.CustomerId);
        command.Parameters.AddWithValue("Balance", account.Balance);

        command.ExecuteNonQuery();
    }

    public void AddTransaction(Transaction transaction)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber ,Amount, Comment, transactionTimeUtc) values (@TransactionType, @AccountNumber, @DestinationAccountNumber,@Amount, @Comment, @transactionTimeUtc)";
        
        command.Parameters.AddWithValue("TransactionType", transaction.TransactionType);
        command.Parameters.AddWithValue("AccountNumber", transaction.AccountNumber);
        command.Parameters.AddWithValue("DestinationAccountNumber", transaction.DestinationAccountNumber != null ? transaction.DestinationAccountNumber : DBNull.Value);
        command.Parameters.AddWithValue("Amount", transaction.Amount);
        command.Parameters.AddWithValue("Comment", transaction.Comment != null ? transaction.Comment : DBNull.Value);
        command.Parameters.AddWithValue("TransactionTimeUtc", transaction.TransactionTimeUtc);

        command.ExecuteNonQuery();
    }

    public void AddLogin(Login login)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Login (LoginID, CustomerID, PasswordHash) values (@LoginID, @CustomerID, @PasswordHash)";
        command.Parameters.AddWithValue("LoginID", login.LoginId);
        command.Parameters.AddWithValue("CustomerID", login.CustomerId);
        command.Parameters.AddWithValue("PasswordHash", login.PasswordHash);
        
        command.ExecuteNonQuery();
    }
}