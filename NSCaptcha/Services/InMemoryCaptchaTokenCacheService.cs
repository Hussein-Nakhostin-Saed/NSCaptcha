using Microsoft.Extensions.Caching.Memory;

namespace NCaptcha;

public class InMemoryCaptchaTokenCacheService : ICaptchaTokenCache
{
    private readonly IMemoryCache _memoryCache;
    public InMemoryCaptchaTokenCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
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
                throw new InvalidCaptchaException("Given token suspicious.this token cached earlier");

            _memoryCache.Remove(key);

            var newToken = new Token(value, DateTime.Now);
            _memoryCache.Set(key, newToken);
        }
    }

    public Token Retrieve(string key)
    {
        if (string.IsNullOrEmpty(key.Trim()))
            throw new InvalidCaptchaException("Captcha cache key is invalid");

        var token = _memoryCache.Get<Token>(key);

        if (token == null || string.IsNullOrEmpty(token.token))
            throw new InvalidCaptchaException("Captcha cache does not exists");

        return token;
    }

    public void Clear(string key)
    {
        if (string.IsNullOrEmpty(key.Trim()))
            throw new InvalidCaptchaException("Captcha cache key is invalid");

        _memoryCache.Remove(key);
    }
}
