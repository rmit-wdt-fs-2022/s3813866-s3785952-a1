namespace Main;

public class StoreCustomerDetails
{
    private static StoreCustomerDetails? _storeCustomerId;
    private int _customerId;
    private string? _customerName;

    private StoreCustomerDetails()
    {
    }

    public static StoreCustomerDetails? GetInstance()
    {
        if (_storeCustomerId == null) _storeCustomerId = new StoreCustomerDetails();
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

    public string? GetCustomerName()
    {
        return _customerName;
    }

    public void SetCustomerName(string? customerName)
    {
        _customerName = customerName;
    }
}