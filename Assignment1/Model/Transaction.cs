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
        /// <summary>
        /// Instance Variables
        /// </summary>
        private int _transactionId;
        private char _transactionType;
        private int _accountNumber;
        private int? _destinationAccountNumber;
        private decimal _amount;
        private string _comment;
        private DateTime _transactionTimeUtc;
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Transaction() { }
    
        /// <summary>
        /// Parameterized Constructor for Transaction Class
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="comment"></param>
        /// <param name="transactionTimeUtc"></param>
        public Transaction(int transactionId, char transactionType, int accountNumber, int DestinationAccountNumber, decimal amount, string comment, DateTime transactionTimeUtc)
        {
            _transactionId = transactionId;
            _transactionType = transactionType;
            _accountNumber = accountNumber;
            _destinationAccountNumber = DestinationAccountNumber;
            _amount = amount;
            _comment = comment;
            _transactionTimeUtc = transactionTimeUtc;
        }

        /// <summary>
        /// Getters and Setters for if we need to update any details for the Transaction Class
        /// </summary>

        public int TransactionID
        {
            get => _transactionId;
            set => _transactionId = value;
        }

        public char TransactionType
        {
            get => _transactionType;
            set => _transactionType = value;
        }

        public int AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }
        public int? DestinationAccountNumber
        {
            get => _destinationAccountNumber;
            set => _destinationAccountNumber = value ?? null;
        }
        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }
    
        public string? Comment
        {
            get => _comment;
            set => _comment = value ?? null;
        }
    
        public DateTime TransactionTimeUtc
        {
            get => _transactionTimeUtc;
            set => _transactionTimeUtc = value;
        }
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