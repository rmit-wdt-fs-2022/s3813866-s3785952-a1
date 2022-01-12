namespace Main.Model;

public class Login
{
    /// <summary>
    /// Instance Variables
    /// </summary>
    private string? _loginId;

    private string? _passwordHash;

    /// <summary>
    /// Default Constructor For Customer
    /// </summary>
    public Login()
    {
    }

    /// <summary>
    /// Parameterized Constructor For Login Class
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="passwordHash"></param>
    public Login(string? loginId, string? passwordHash)
    {
        _loginId = loginId;
        _passwordHash = passwordHash;
    }

    /// <summary>
    /// Getters and Setters for if we need to update any details for the Login Class
    /// </summary>
    public string? LoginId
    {
        get => _loginId;
        set => _loginId = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string? PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = value ?? throw new ArgumentNullException(nameof(value));
    }
}