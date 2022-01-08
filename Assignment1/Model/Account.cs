
namespace Main
{
    public class Account
    {
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }

        public long CustomerID { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
