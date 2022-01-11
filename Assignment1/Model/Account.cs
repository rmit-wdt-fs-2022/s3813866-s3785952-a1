using Main.Model;
    public class Account
    {
        /// <summary>
        /// Instance Variables
        /// </summary>
        private int _accountNumber;
        private char _accountType;
        private int _customerId;
        private decimal _balance;
        private List<Transaction> _transactions = new();
        
        /// <summary>
        /// Parameterized Constructor for Account Class
        /// </summary>
        public Account() { }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountType"></param>
        /// <param name="customerId"></param>
        /// <param name="transactions"></param>
        public Account(int accountNumber, char accountType, int customerId, decimal balance, List<Transaction> transactions)
        {
            _accountNumber = accountNumber;
            _accountType = accountType;
            _customerId = customerId;
            _balance = balance;
            _transactions = transactions;
        }
    
        /// <summary>
        /// Getters and Setters for if we need to update any details for the Account Class
        /// </summary>
        public int AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }
    
        public char AccountType
        {
            get => _accountType;
            set => _accountType = value;
        }
    
        public int CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }
        
        public decimal? Balance
        {
            get => _balance;
            set => _balance = value ?? throw new ArgumentNullException(nameof(value));
        }
    
        public List<Transaction> Transactions
        {
            get => _transactions;
            set => _transactions = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
