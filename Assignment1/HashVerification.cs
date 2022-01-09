namespace Main;
using SimpleHashing;

public static class HashVerification
{
    public static bool PasswordCheck(string hash, string inputPw)
    {
        return PBKDF2.Verify(hash, inputPw);
    }
}