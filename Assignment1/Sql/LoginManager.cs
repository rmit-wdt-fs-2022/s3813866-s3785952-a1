using System.Data;
using Main.Model;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public class LoginManager : IManager<Login>
{
    private readonly string _connectionString;

    public LoginManager(string connectionString)
    {
        _connectionString = connectionString;
    }
        
    public void Add(Login login)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "insert into Login (LoginID, CustomerID, PasswordHash) values (@LoginID, @CustomerID, @PasswordHash)";
            command.Parameters.AddWithValue("LoginID", login.LoginId);
            command.Parameters.AddWithValue("CustomerID", login.CustomerId);
            command.Parameters.AddWithValue("PasswordHash", login.PasswordHash);

            command.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
            Console.WriteLine("Add login unsuccessful");
        }
    }

    public List<Login> CheckTable()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from Login";

            return command.GetDataTable().Select().Select(x => new Login
            {
                LoginId = x.Field<string>("LoginID"),
                CustomerId = x.Field<int>("CustomerID"),
                PasswordHash = x.Field<string>("PasswordHash")
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
        
    }
    
    public Login GetLogin(int loginId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from [Login] where LoginID = @LoginID";
            command.Parameters.AddWithValue("LoginID", loginId);

            var list = command.GetDataTable().Select().Select(x => new Login
            {
                LoginId = x.Field<string>("LoginID"),
                CustomerId = x.Field<int>("CustomerID"),
                PasswordHash = x.Field<string>("PasswordHash")
            }).ToList();
            return list.First();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine("Unavailable");
            return null;
        }
    }
}