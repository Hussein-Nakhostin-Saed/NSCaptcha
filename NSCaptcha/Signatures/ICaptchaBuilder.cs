namespace NSCaptcha;

/// <summary>
/// Defines the interface for a builder pattern used to configure and construct a Captcha generation system.
/// This interface provides methods for registering the necessary services, such as token serialization, 
/// encryption, and caching.
/// </summary>
public interface ICaptchaBuilder
{
    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenSerializer"/> to handle 
    /// the serialization and deserialization of Captcha tokens.
    /// </summary>
    /// <typeparam name="TSerializer">The type of the <see cref="ICaptchaTokenSerializer"/> implementation.</typeparam>
    /// <returns>The current <see cref="ICaptchaBuilder"/> instance for chaining method calls.</returns>
    ICaptchaBuilder AddCaptchaTokenSerializer<TSerializer>() where TSerializer : ICaptchaTokenSerializer;

    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenProtector"/> to handle 
    /// the encryption and decryption of Captcha tokens.
    /// </summary>
    /// <typeparam name="TEncryptionService">The type of the <see cref="ICaptchaTokenProtector"/> implementation.</typeparam>
    /// <returns>The current <see cref="ICaptchaBuilder"/> instance for chaining method calls.</returns>
    ICaptchaBuilder AddCaptchaTokenEncryptionService<TEncryptionService>() where TEncryptionService : ICaptchaTokenProtector;

    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenCache"/> to handle 
    /// the caching of Captcha tokens.
    /// </summary>
    /// <typeparam name="TCaptchaTokenCacheService">The type of the <see cref="ICaptchaTokenCache"/> implementation.</typeparam>
    /// <returns>The current <see cref="ICaptchaBuilder"/> instance for chaining method calls.</returns>
    ICaptchaBuilder AddCaptchaTokenCacheService<TCaptchaTokenCacheService>() where TCaptchaTokenCacheService : ICaptchaTokenCache;
}