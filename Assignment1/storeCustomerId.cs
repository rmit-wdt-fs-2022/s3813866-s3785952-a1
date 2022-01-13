namespace Main;

public class StoreCustomerId
{
    private static StoreCustomerId? _storeCustomerId;
    private int _customerId;

    private StoreCustomerId()
    {
    }

    public static StoreCustomerId? GetInstance()
    {
        if (_storeCustomerId == null) _storeCustomerId = new StoreCustomerId();
        return _storeCustomerId;
    }

    public int GetCustomerId()
    {
        return _customerId;
    }

    public void SetCustomerId(int customerId)
    {
        _customerId = customerId;
    }
}