using System.Data;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class AccountManager : IManager<Account>
{
    private readonly string _connectionString;

    public AccountManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(Account account)
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

    public List<Account> CheckTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Account ";


        return command.GetDataTable().Select().Select(x => new Account
        {
            AccountNumber = x.Field<int>("AccountNumber"),
            //Assuming the web service always return valid data, 'T' will serve as a default value
            AccountType = x.Field<string>("AccountType")?.Single() ?? 'S',
            CustomerId = x.Field<int>("CustomerID"),
            Balance = x.Field<decimal>("Balance")
        }).ToList();
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

    public Account GetAccount(int customerId, int accountNumber)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM Account WHERE CustomerID = @CustomerID AND AccountNumber = @AccountNumber";
            command.Parameters.AddWithValue("CustomerID", customerId);
            command.Parameters.AddWithValue("AccountNumber", accountNumber);

            var account = command.GetDataTable().Select().Select(x => new Account
            {
                AccountNumber = x.Field<int>("AccountNumber"),
                //Assuming the web service always return valid data, 'T' will serve as a default value
                AccountType = x.Field<string>("AccountType")?.Single() ?? 'S',
                CustomerId = customerId,
                Balance = x.Field<decimal>("Balance")
            }).ToList();

            return account.First();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public void UpdateBalance(int accountNum, decimal newBalance)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            "UPDATE Account SET Balance = @Balance WHERE AccountNumber = @AccountNumber";
        command.Parameters.AddWithValue("Balance", newBalance);
        command.Parameters.AddWithValue("AccountNumber", accountNum);

        command.ExecuteNonQuery();
    }
}