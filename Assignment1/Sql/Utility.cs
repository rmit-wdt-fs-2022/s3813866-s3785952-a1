using System.Data;
using Microsoft.Data.SqlClient;

namespace Main.Sql;

public static class Utility
{
    public static DataTable GetDataTable(this SqlCommand command)
    {
        using var adapter = new SqlDataAdapter(command);
    
        var table = new DataTable();
        adapter.Fill(table);
    
        return table;
    } 
}