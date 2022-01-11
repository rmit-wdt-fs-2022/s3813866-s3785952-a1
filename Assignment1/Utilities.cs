using SimpleHashing;

namespace Main;

public static class Utilities
{
    public static bool HashVerification(string passwordHash, string password)
    {
        return PBKDF2.Verify(passwordHash, password);
    }

    public static bool CheckStringIsAnInt(string s)
    {
        switch (s)
        {
            case null:
            case "":
                return false;
        }

        return int.TryParse(s, out var i);
    }

    public static int ConvertToInt32(string s)
    {
        return int.Parse(s);
    }
}