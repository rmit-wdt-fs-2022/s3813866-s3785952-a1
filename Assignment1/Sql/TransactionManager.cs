﻿using System.Data;
using Main.Model;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class TransactionManager : IManager<Transaction>
{
    private const char transactionTypeDeposit = 'D';
    private const char transactionTypeWithdraw = 'W';
    private const char transactionTypeTransfer = 'T';
    private readonly string _connectionString;

    public TransactionManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(Transaction transaction)
    {
        try
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
            command.Parameters.AddWithValue("Comment",
                transaction.Comment != null ? transaction.Comment : DBNull.Value);
            command.Parameters.AddWithValue("TransactionTimeUtc", transaction.TransactionTimeUtc);

            command.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
            Console.WriteLine("Add transaction unsuccessful");
        }
    }

    public List<Transaction> CheckTable()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from [Transaction]";

            return command.GetDataTable().Select().Select(x => new Transaction
            {
                TransactionID = x.Field<int>("TransactionID"),
                //Assuming the web service always return valid data, 'D' will serve as a default value
                TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionTypeDeposit,
                AccountNumber = x.Field<int>("AccountNumber"),
                DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
                Amount = x.Field<decimal>("Amount"),
                Comment = x.Field<string?>("Comment"),
                TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
        
        
    }

    
    public List<Transaction> GetWithdrawTransactions(int accountNum)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM [Transaction] WHERE TransactionType = @AccountType AND accountnumber = @AccountNumber";
            command.Parameters.AddWithValue("AccountNumber", accountNum);
            command.Parameters.AddWithValue("AccountType", transactionTypeWithdraw);
        
            return command.GetDataTable().Select().Select(x => new Transaction
            {
                TransactionID = x.Field<int>("TransactionID"),
                //Assuming the web service always return valid data, 'D' will serve as a default value
                TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionTypeDeposit,
                AccountNumber = accountNum,
                DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
                Amount = x.Field<decimal>("Amount"),
                Comment = x.Field<string?>("Comment"),
                TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
       
        
    }
    
    public List<Transaction> GetTransferTransactions(int accountNum)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM [Transaction] WHERE TransactionType = @AccountType AND DestinationAccountNumber != null AND accountnumber = @AccountNumber";
            command.Parameters.AddWithValue("AccountNumber", accountNum);
            command.Parameters.AddWithValue("AccountType", transactionTypeTransfer);
        
            return command.GetDataTable().Select().Select(x => new Transaction
            {
                TransactionID = x.Field<int>("TransactionID"),
                //Assuming the web service always return valid data, 'D' will serve as a default value
                TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionTypeDeposit,
                AccountNumber = accountNum,
                DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
                Amount = x.Field<decimal>("Amount"),
                Comment = x.Field<string?>("Comment"),
                TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
        
    }
    
    /// <summary>
    /// This method will return a list of transaction from a given user, it can also
    /// be used to keep track of how many transfers an account has made.
    /// <param name="accountNum"></param>
    /// </summary>
    /// 
    public List<Transaction> GetTransaction(int accountNum)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from [Transaction] where AccountNumber = @AccountNumber";
            command.Parameters.AddWithValue("AccountNumber", accountNum);

            return command.GetDataTable().Select().Select(x => new Transaction
            {
                TransactionID = x.Field<int>("TransactionID"),
                //Assuming the web service always return valid data, 'D' will serve as a default value
                TransactionType = x.Field<string>("TransactionType")?.Single() ?? transactionTypeDeposit,
                AccountNumber = accountNum,
                DestinationAccountNumber = x.Field<int?>("DestinationAccountNumber"),
                Amount = x.Field<decimal>("Amount"),
                Comment = x.Field<string?>("Comment"),
                TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
            }).OrderByDescending(x => x.TransactionTimeUtc).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
        
    }
}