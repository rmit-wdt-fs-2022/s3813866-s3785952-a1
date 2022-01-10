using SimpleHashing;

namespace Main;

public static class Utilities
{
    public static bool HashVerification(string passwordHash, string password)
    {
        return PBKDF2.Verify(passwordHash, password);
    }
    
}