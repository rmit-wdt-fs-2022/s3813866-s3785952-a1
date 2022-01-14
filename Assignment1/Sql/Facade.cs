using Main.Model;

namespace Main.Sql;

public class Facade
{
    private AccountManager accountManager;
    private TransactionManager transactionManager;

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

    public void Withdraw(int accountNum, decimal amount, string comment, decimal balance)
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

    public void Transfer(int accountNum, int destinationAccount, decimal amount, string comment, decimal balance)
    {
        char transactionType = 'T';
        DateTime timeStamp = DateTime.UtcNow;
        var newTransaction = new Transaction();
        newTransaction.AccountNumber = accountNum;
        newTransaction.Amount = amount;
        newTransaction.TransactionType = transactionType;
        newTransaction.DestinationAccountNumber = destinationAccount;
        newTransaction.Comment = comment;
        newTransaction.TransactionTimeUtc = timeStamp;

        transactionManager.Add(newTransaction);
        accountManager.UpdateBalance(accountNum, balance);
    }

}