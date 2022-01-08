

namespace Main
{
    public class Customer
    {

        public long CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public List<Account> accounts { get; set; }
        
        public LoginInfo Login { get; set; }


    }
}
