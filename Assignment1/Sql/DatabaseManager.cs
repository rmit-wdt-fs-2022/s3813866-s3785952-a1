using System.Data;
using Main.Model;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class DatabaseManager
{
    private const char transactionType = 'D';
    private const char accountType = 'T';
    private readonly string _connectionString;

    public DatabaseManager()
    {
    }

    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;

        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";

        Customers = command.GetDataTable().Select().Select(x => new Customer
        {
            CustomerId = x.Field<int>("CustomerID"),
            Name = x.Field<string?>("Name"),
            Address = x.Field<string>("Address"),
            City = x.Field<string>("City")
        }).ToList();
    }

    public List<Customer> Customers { get; }

    public void AddCustomer(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Customer (CustomerID, Name, Address, City, Postcode) values (@CustomerID, @Name, @Address, @City, @Postcode)";
        command.Parameters.AddWithValue("CustomerID", customer.CustomerId);
        command.Parameters.AddWithValue("Name", customer.Name);
        command.Parameters.AddWithValue("Address", customer.Address != null ? customer.Address : DBNull.Value);
        command.Parameters.AddWithValue("City", customer.City != null ? customer.City : DBNull.Value);
        command.Parameters.AddWithValue("Postcode", customer.PostCode != null ? customer.PostCode : DBNull.Value);

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
        command.Parameters.AddWithValue("DestinationAccountNumber",
            transaction.DestinationAccountNumber != null ? transaction.DestinationAccountNumber : DBNull.Value);
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

    ///TODO: make sure to surround these methods with a try catch, if they don't find anything in the db it will throw a
    /// System.InvalidOperationException
    public Login GetLogin(int customerId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Login where CustomerID = @CustomerID";
        command.Parameters.AddWithValue("CustomerID", customerId);

        var list = command.GetDataTable().Select().Select(x => new Login
        {
            LoginId = x.Field<string>("LoginID"),
            CustomerId = customerId,
            PasswordHash = x.Field<string>("PasswordHash")
        }).ToList();

        return list.First();
    }

    /// <summary>
    /// This method will return a list of transaction from a given user, it can also
    /// be used to keep track of how many transfers an account has made.
    /// <param name="accountNum"></param>
    /// </summary>
    public List<Transaction> GetTransaction(int accountNum)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from [Transaction] where AccountNumber = @AccountNumber";
        command.Parameters.AddWithValue("AccountNumber", accountNum);

        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionID = x.Field<int>("TransactionID"),
            //Assuming the web service always return valid data, 'D' will serve as a default value
            TransactionType = x.Field<string>("TransactionType")?.Single() ?? 'D',
            AccountNumber = accountNum,
            DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string?>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).OrderByDescending(x => x.TransactionTimeUtc).ToList();
    }

    public List<Account> GetAccounts(int customerId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Account where CustomerID = @CustomerID";
        command.Parameters.AddWithValue("CustomerID", customerId);

        return command.GetDataTable().Select().Select(x => new Account
        {
            AccountNumber = x.Field<int>("AccountNumber"),
            //Assuming the web service always return valid data, 'T' will serve as a default value
            AccountType = x.Field<string>("AccountType")?.Single() ?? 'S',
            CustomerId = customerId,
            Balance = x.Field<decimal>("Balance")
        }).ToList();
    }
}