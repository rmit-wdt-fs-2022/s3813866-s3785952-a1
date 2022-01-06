
namespace Main
{
    public class Account
    {
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }

        public long CustomerID { get; set; }

        public Transaction Transactions { get; set; }
    }
}
