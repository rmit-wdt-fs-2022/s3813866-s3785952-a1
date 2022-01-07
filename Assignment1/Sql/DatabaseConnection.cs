using Microsoft.Data.SqlClient;
namespace Main.Sql;

public static class DatabaseConnection
{
    public static void CreateTables(string connectionString)
    {
        using var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        using var command = sqlConnection.CreateCommand();
        command.CommandText = File.ReadAllText("Sql/CreateTables.sql");

        command.ExecuteNonQuery();
    }
}