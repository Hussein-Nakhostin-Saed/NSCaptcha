using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// Defines the interface for caching Captcha tokens.
/// Implementations of this interface provide a mechanism to temporarily store and retrieve Captcha tokens,
/// typically to prevent reuse or abuse of Captcha challenges.  Different caching mechanisms (e.g., in-memory,
/// distributed cache, database) can be implemented.
/// </summary>
public interface ICaptchaTokenCache
{
    /// <summary>
    /// Caches the specified Captcha token with the given key.
    /// </summary>
    /// <param name="key">The key to associate with the token.</param>
    /// <param name="value">The Captcha token to cache.</param>
    /// <exception cref="CaptchaException">Thrown when an error occurs during caching 
    /// (e.g., storage failure, invalid input).</exception>
    void Cache(string key, string value);

    /// <summary>
    /// Retrieves the Captcha token associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the token to retrieve.</param>
    /// <returns>The cached Captcha token, or null if no token is found for the given key.</returns>
    /// <exception cref="CaptchaException">Thrown when an error occurs during retrieval
    /// (e.g., storage access failure, invalid key).</exception>
    Token Retrieve(string key);

    /// <summary>
    /// Clears the cached Captcha token associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the token to clear.</param>
    /// <exception cref="CaptchaException">Thrown when an error occurs during clearing
    /// (e.g., storage access failure, invalid key).</exception>
    void Clear(string key);
}