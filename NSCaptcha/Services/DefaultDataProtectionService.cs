using Microsoft.AspNetCore.DataProtection;
using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// Implements the <see cref="ICaptchaTokenProtector"/> interface using ASP.NET Core Data Protection for encryption and decryption.
/// This class provides a default implementation for protecting Captcha tokens using a specific protection key.
/// </summary>
public class DefaultDataProtectionService : ICaptchaTokenProtector
{
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly string _protectionKey = "CaptchaTokenEncryption";

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultDataProtectionService"/> class.
    /// </summary>
    /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/> used to create data protectors.</param>
    public DefaultDataProtectionService(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtectionProvider = dataProtectionProvider;
    }

    /// <summary>
    /// Decrypts the provided token using the configured data protector.
    /// </summary>
    /// <param name="token">The encrypted token to decrypt.</param>
    /// <returns>The decrypted token.</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Thrown when an error occurs during decryption 
    /// (e.g., invalid token, tampering).</exception>
    public string Decrypt(string token)
    {
        var protector = _dataProtectionProvider.CreateProtector(_protectionKey);
        var decryptedToken = protector.Unprotect(token);

        return decryptedToken;
    }

    /// <summary>
    /// Encrypts the provided token using the configured data protector.
    /// </summary>
    /// <param name="token">The token to encrypt.</param>
    /// <returns>The encrypted token.</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Thrown when an error occurs during encryption
    /// (e.g., key not found, algorithm failure).</exception>
    public string Encrypt(string token)
    {
        var protector = _dataProtectionProvider.CreateProtector(_protectionKey);
        var encryptedToken = protector.Protect(token);

        return encryptedToken;
    }
}