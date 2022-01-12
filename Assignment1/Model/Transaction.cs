namespace Main.Model;

public class Transaction
{
    /// <summary>
    /// Instance Variables
    /// </summary>
    private double _amount;

    private string? _comment;
    private string? _transactionTimeUtc;

    /// <summary>
    /// Default Constructor
    /// </summary>
    public Transaction()
    {
    }

    /// <summary>
    /// Parameterized Constructor for Transaction Class
    /// </summary>
    /// <param name="transactionId"></param>
    /// <param name="amount"></param>
    /// <param name="comment"></param>
    /// <param name="transactionTimeUtc"></param>
    public Transaction(double amount, string? comment, string? transactionTimeUtc)
    {
        _amount = amount;
        _comment = comment;
        _transactionTimeUtc = transactionTimeUtc;
    }

    /// <summary>
    /// Getters and Setters for if we need to update any details for the Transaction Class
    /// </summary>
    public double Amount
    {
        get => _amount;
        set => _amount = value;
    }

    public string? Comment
    {
        get => _comment;
        set => _comment = value ?? null;
    }

    public string? TransactionTimeUtc
    {
        get => _transactionTimeUtc;
        set => _transactionTimeUtc = value ?? throw new ArgumentNullException(nameof(value));
    }
}