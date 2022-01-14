using System.Text;
using Main.Model;

namespace Main.Sql;

public class Facade
{
    private AccountManager accountManager;
    private TransactionManager transactionManager;
    private const char withdraw = 'W';
    private const char service = 'S';
    private const char transfer = 'T';

    public Facade(string connectionString)
    {
        new CustomerManager(connectionString);
        accountManager = new AccountManager(connectionString);
        transactionManager = new TransactionManager(connectionString);
    }

    public Account? GetUserAccounts(int customerId, int accountNum)
    {
        if (accountManager.CheckTable().Any())
        {
            return accountManager.GetAccount(customerId, accountNum);
        }

        return null;
    }

    public void Deposit(int accountNumberSelected, decimal amount, string? comment, decimal balance)
    {
        char transactionType = 'D';
        DateTime timeStamp = DateTime.UtcNow;
        var newTransaction = new Transaction();
        newTransaction.AccountNumber = accountNumberSelected;
        newTransaction.Amount = amount;
        newTransaction.TransactionType = transactionType;
        newTransaction.Comment = comment;
        newTransaction.TransactionTimeUtc = timeStamp;

        transactionManager.Add(newTransaction);
        accountManager.UpdateBalance(accountNumberSelected, balance);
    }

    public int NumberOfTransactions(int accountNum)
    {
        var transfer = transactionManager.GetTransferTransactions(accountNum).Count;
        var withdraw = transactionManager.GetWithdrawTransactions(accountNum).Count;

        return transfer + withdraw;
    }

    public void Withdraw(int accountNum, decimal amount, string? comment, decimal balance)
    {
        char transactionType = 'W';
        DateTime timeStamp = DateTime.UtcNow;
        var newTransaction = new Transaction();
        newTransaction.AccountNumber = accountNum;
        newTransaction.Amount = amount;
        newTransaction.TransactionType = transactionType;
        newTransaction.Comment = comment;
        newTransaction.TransactionTimeUtc = timeStamp;

        transactionManager.Add(newTransaction);
        accountManager.UpdateBalance(accountNum, balance);
    }
    
    public void WithdrawWithFee(int accountNum, decimal amount, string? comment, decimal balance, decimal fee)
    {
        
        DateTime timeStamp = DateTime.UtcNow;
        var newTransaction = new Transaction();
        var serviceTransaction = new Transaction();
        
        newTransaction.AccountNumber = accountNum;
        newTransaction.Amount = amount;
        newTransaction.TransactionType = withdraw;
        newTransaction.Comment = comment;
        newTransaction.TransactionTimeUtc = timeStamp;
        
        serviceTransaction.AccountNumber = accountNum;
        serviceTransaction.Amount = fee;
        serviceTransaction.TransactionType = service;
        serviceTransaction.TransactionTimeUtc = timeStamp;

        transactionManager.Add(newTransaction);
        transactionManager.Add(serviceTransaction);
        accountManager.UpdateBalance(accountNum, balance);
    }

    public void Transfer(int accountNum, int destinationAccount, decimal amount, string comment, decimal senderBalance , decimal receiverBalance)
    {
        
        DateTime timeStamp = DateTime.UtcNow;
        var sender = new Transaction();
        var receiver = new Transaction();

        sender.AccountNumber = accountNum;
        sender.Amount = amount;
        sender.TransactionType = transfer;
        sender.DestinationAccountNumber = destinationAccount;
        sender.Comment = comment;
        sender.TransactionTimeUtc = timeStamp;
        
        receiver.AccountNumber = destinationAccount;
        receiver.Amount = amount;
        receiver.TransactionType = transfer;
        receiver.Comment = comment;
        receiver.TransactionTimeUtc = timeStamp;
        
        
        transactionManager.Add(sender);
        transactionManager.Add(receiver);
        accountManager.UpdateBalance(accountNum, senderBalance);
        accountManager.UpdateBalance(destinationAccount, receiverBalance);
        
    }
    public void TransferWithFee(int accountNum, int destinationAccount, decimal amount, string comment, decimal senderBalance , decimal receiverBalance, decimal fee)
    {
        DateTime timeStamp = DateTime.UtcNow;
        var sender = new Transaction();
        var receiver = new Transaction();
        var serviceTransaction = new Transaction();
        
        sender.AccountNumber = accountNum;
        sender.Amount = amount;
        sender.TransactionType = transfer;
        sender.DestinationAccountNumber = destinationAccount;
        sender.Comment = comment;
        sender.TransactionTimeUtc = timeStamp;
        
        receiver.AccountNumber = destinationAccount;
        receiver.Amount = amount;
        receiver.TransactionType = transfer;
        receiver.Comment = comment;
        receiver.TransactionTimeUtc = timeStamp;
        
        serviceTransaction.AccountNumber = accountNum;
        serviceTransaction.Amount = fee;
        serviceTransaction.TransactionType = service;
        serviceTransaction.TransactionTimeUtc = timeStamp;
        
        transactionManager.Add(sender);
        transactionManager.Add(receiver);
        transactionManager.Add(serviceTransaction);
        accountManager.UpdateBalance(accountNum, senderBalance);
        accountManager.UpdateBalance(destinationAccount,receiverBalance);
        
    }
}