using System.Data;
using Main.Model;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class CustomerManager : IManager<Customer>
{
    private readonly string _connectionString;

    public CustomerManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Customer> Customers { get; }


    public void Add(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            "insert into Customer (CustomerID, Name, Address, City, Postcode) values (@CustomerID, @Name, @Address, @City, @Postcode)";
        command.Parameters.AddWithValue("CustomerID", customer.CustomerId);
        command.Parameters.AddWithValue("Name", customer.Name);
        command.Parameters.AddWithValue("Address", customer.Address != null ? customer.Address : DBNull.Value);
        command.Parameters.AddWithValue("City", customer.City != null ? customer.City : DBNull.Value);
        command.Parameters.AddWithValue("Postcode", customer.PostCode != null ? customer.PostCode : DBNull.Value);

        command.ExecuteNonQuery();
    }

    public List<Customer> CheckTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";

        return command.GetDataTable().Select().Select(x => new Customer
        {
            CustomerId = x.Field<int>("CustomerID"),
            Name = x.Field<string?>("Name"),
            Address = x.Field<string>("Address"),
            City = x.Field<string>("City")
        }).ToList();
    }

    public Customer GetName(int customerId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from Customer where CustomerID = @CustomerID";
            command.Parameters.AddWithValue("CustomerID", customerId);

            var list = command.GetDataTable().Select().Select(x => new Customer
            {
                CustomerId = x.Field<int>("CustomerID"),
                Name = x.Field<string?>("Name"),
                Address = x.Field<string>("Address"),
                City = x.Field<string>("City")
            }).ToList();

            return list.First();
        }
        catch (InvalidOperationException exception)
        {
            return null;
        }
    }
}