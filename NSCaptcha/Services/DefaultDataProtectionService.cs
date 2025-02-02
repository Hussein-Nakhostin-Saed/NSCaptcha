using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace NCaptcha;

public class DefaultDataProtectionService : ICaptchaTokenProtector
{
    private readonly IDataProtectionProvider _dataProtectionProvider;
    public DefaultDataProtectionService(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtectionProvider = dataProtectionProvider;
    }

    public string Decrypt(string token)
    {
        var protector = _dataProtectionProvider.CreateProtector("CaptchaTokenEncryption");
        var decryptedToken = protector.Unprotect(token);

        return decryptedToken;
    }

    public string Encrypt(string token)
    {
        var protector = _dataProtectionProvider.CreateProtector("CaptchaTokenEncryption");
        var encryptedToken = protector.Protect(token);
        
        return encryptedToken;
    }
}
