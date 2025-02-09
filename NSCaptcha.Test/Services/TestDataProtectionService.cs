namespace NSCaptcha.Test.Services;

public class TestDataProtectionService : ICaptchaTokenProtector
{
    public string Decrypt(string token)
    {
        throw new NotImplementedException();
    }

    public string Encrypt(string token)
    {
        throw new NotImplementedException();
    }
}
