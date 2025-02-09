namespace NSCaptcha;

/// <summary>
/// Defines the interface for encrypting and decrypting Captcha tokens.
/// Implementations of this interface provide a mechanism to protect Captcha tokens 
/// from tampering or unauthorized access.
/// </summary>
public interface ICaptchaTokenProtector
{
    /// <summary>
    /// Encrypts the provided token.
    /// </summary>
    /// <param name="token">The token to encrypt.</param>
    /// <returns>The encrypted token.</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Thrown when an error occurs during encryption 
    /// (e.g., invalid token, key not found, algorithm failure).</exception>
    string Encrypt(string token);

    /// <summary>
    /// Decrypts the provided token.
    /// </summary>
    /// <param name="token">The encrypted token to decrypt.</param>
    /// <returns>The decrypted token.</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Thrown when an error occurs during decryption 
    /// (e.g., invalid token, tampering, key not found).</exception>
    string Decrypt(string token);
}