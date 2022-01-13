namespace Main;

public class StoreConnectionString
{
    private static StoreConnectionString? _storeConnectionString;
    private string? _connectionString;

    private StoreConnectionString()
    {
    }

    public static StoreConnectionString? GetInstance()
    {
        if (_storeConnectionString == null) _storeConnectionString = new StoreConnectionString();
        return _storeConnectionString;
    }

    public string? GetConnectionString()
    {
        return _connectionString;
    }

    public void SetConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
    }
}