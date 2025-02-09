using Microsoft.Extensions.Caching.Memory;
using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// Implements the <see cref="ICaptchaTokenCache"/> interface using an in-memory cache (<see cref="IMemoryCache"/>).
/// This class provides a cache for storing and retrieving Captcha tokens, leveraging the in-memory caching capabilities.
/// </summary>
public class InMemoryCaptchaTokenCacheService : ICaptchaTokenCache
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryCaptchaTokenCacheService"/> class.
    /// </summary>
    /// <param name="memoryCache">The <see cref="IMemoryCache"/> instance to use for caching.</param>
    public InMemoryCaptchaTokenCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Caches the specified Captcha token with the given key.  If a token already exists for the given key,
    /// and the token value is the same as the new value, a <see cref="CaptchaException"/> is thrown. If the 
    /// token is different, the existing token is removed and the new token is cached.
    /// </summary>
    /// <param name="key">The key to associate with the token.</param>
    /// <param name="value">The Captcha token to cache.</param>
    /// <exception cref="CaptchaException">Thrown if the given token is identical to a previously cached token for the same key, indicating a suspicious attempt.</exception>
    public void Cache(string key, string value)
    {
        if (!_memoryCache.TryGetValue(key, out string token))
        {
            var newToken = new Token(value, DateTime.Now);
            _memoryCache.Set(key, newToken);
        }
        else
        {
            if (token == value)
                throw new CaptchaException("Given token suspicious. This token cached earlier.");

            _memoryCache.Remove(key); // Remove the old token before caching the new one
            var newToken = new Token(value, DateTime.Now);
            _memoryCache.Set(key, newToken);
        }
    }

    /// <summary>
    /// Retrieves the Captcha token associated with the specified key from the cache.
    /// </summary>
    /// <param name="key">The key of the token to retrieve.</param>
    /// <returns>The cached <see cref="Token"/> object.</returns>
    /// <exception cref="CaptchaException">Thrown if the provided key is invalid (null or empty) or if no token is found in the cache for the given key.</exception>
    public Token Retrieve(string key)
    {
        if (string.IsNullOrEmpty(key.Trim()))
            throw new CaptchaException("Captcha cache key is invalid");

        var token = _memoryCache.Get<Token>(key);

        if (token == null || string.IsNullOrEmpty(token.token))
            throw new CaptchaException("Captcha cache does not exist");

        return token;
    }

    /// <summary>
    /// Clears the Captcha token associated with the specified key from the cache.
    /// </summary>
    /// <param name="key">The key of the token to clear.</param>
    /// <exception cref="CaptchaException">Thrown if the provided key is invalid (null or empty).</exception>
    public void Clear(string key)
    {
        if (string.IsNullOrEmpty(key.Trim()))
            throw new CaptchaException("Captcha cache key is invalid");

        _memoryCache.Remove(key);
    }
}