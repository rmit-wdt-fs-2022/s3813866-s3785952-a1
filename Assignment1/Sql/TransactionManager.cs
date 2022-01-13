using System.Data;
using Main.Model;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class TransactionManager : IManager<Transaction>
{
    private const char transactionType = 'D';
    private readonly string _connectionString;

    public TransactionManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(Transaction transaction)
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

    public List<Transaction> CheckTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from [Transaction]";

        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionID = x.Field<int>("TransactionID"),
            //Assuming the web service always return valid data, 'D' will serve as a default value
            TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionType,
            AccountNumber = x.Field<int>("AccountNumber"),
            DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string?>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).OrderByDescending(x => x.TransactionTimeUtc).ToList();
    }

    public List<Transaction> GetNonDepositTransactions(int accountNum)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from [Transaction] where AccountNumber = @AccountNumber and TransactionType != @AccountType";
        command.Parameters.AddWithValue("AccountNumber", accountNum);
        command.Parameters.AddWithValue("AccountType", transactionType);
        
        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionID = x.Field<int>("TransactionID"),
            //Assuming the web service always return valid data, 'D' will serve as a default value
            TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionType,
            AccountNumber = accountNum,
            DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string?>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).OrderByDescending(x => x.TransactionTimeUtc).ToList();
    }
    
    /// <summary>
    /// This method will return a list of transaction from a given user, it can also
    /// be used to keep track of how many transfers an account has made.
    /// <param name="accountNum"></param>
    /// </summary>
    /// 
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
            TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionType,
            AccountNumber = accountNum,
            DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string?>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).OrderByDescending(x => x.TransactionTimeUtc).ToList();
    }
}