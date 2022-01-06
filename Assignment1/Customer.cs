

namespace Main
{
    public class Customer
    {

        public long CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public Account accounts { get; set; }


    }
}
