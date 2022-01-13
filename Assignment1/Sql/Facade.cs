using Main.Model;

namespace Main.Sql;

public class Facade
{

    private LoginManager loginManager;
    private CustomerManager customerManager;
    private AccountManager accountManager;
    private TransactionManager transactionManager;
    public Facade(string connectionString)
    {
        loginManager = new LoginManager(connectionString);
        customerManager = new CustomerManager(connectionString);
        accountManager = new AccountManager(connectionString);
        transactionManager = new TransactionManager(connectionString);
    }
    public Account GetUserAccounts(int customerID, int accountNum)
    {
        if (accountManager.CheckTable().Any())
        {
            return accountManager.GetAccount(customerID, accountNum);
        }
        return null;
    }

    public void Deposit(int accountNumberSelected, int amount, string comment, decimal balance)
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
    
}