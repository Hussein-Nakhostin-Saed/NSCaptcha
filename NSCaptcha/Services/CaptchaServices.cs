using Microsoft.Extensions.DependencyInjection;
using NSCaptcha.Utilities;

namespace NSCaptcha;

public class CaptchaService : ICaptchaService
{
    private readonly InternalCaptchaOptions _options;
    private Counter _counter = Counter.Instance;
    private readonly IImageDrawer _imageDraw;
    private readonly ICaptchaTokenSerializer _tokenSerializer;
    private readonly ICaptchaTokenProtector _captchaTokenEncrypt;
    private readonly ICaptchaTokenCache _captchaTokenCache;
    private string _tokenCacheKey = "captchaData";
    private Token _token;

    public CaptchaService(IImageDrawer imageDrawer,
                          [FromKeyedServices("TokenSerializer")] ICaptchaTokenSerializer tokenSerializer,
                          [FromKeyedServices("DataProtection")] ICaptchaTokenProtector captchaTokenEncrypt,
                          InternalCaptchaOptions options,
                          [FromKeyedServices("TokenCache")] ICaptchaTokenCache captchaTokenCache)
    {
        _options = options;
        _imageDraw = imageDrawer;
        _tokenSerializer = tokenSerializer;
        _captchaTokenCache = captchaTokenCache;
        _captchaTokenEncrypt = captchaTokenEncrypt;
    }

    /// <summary>
    /// This method generates a new Captcha challenge. It creates a random string, encrypts it, caches it, 
    /// draws the text as an image, and returns a Captcha object containing the image, the original string, 
    /// and the encrypted token.
    /// </summary>
    /// <returns>A new Captcha object.</returns>
    public Captcha Create()
    {
        _counter.Reset();
        var randomStringModel = new RandomStringModel(_options.IncludeUpperCaseLetters,
                                                      _options.IncludeLowerCaseLetters,
                                                      _options.IncludeDigits,
                                                      _options.IncludeSymbols,
                                                      _options.Length);
        var randomString = Utils.CreateRandomStringAsync(randomStringModel);
        var hashString = _captchaTokenEncrypt.Encrypt(randomString);
        _captchaTokenCache.Cache(_tokenCacheKey, hashString);
        _tokenSerializer.Serialize(hashString);
        var imageAsbyteArray = _imageDraw.DrawText(randomString);

        return new Captcha(imageAsbyteArray, randomString, hashString);
    }

    /// <summary>
    /// This method validates a submitted Captcha value against the currently generated Captcha token. 
    /// It retrieves the cached and cookie values, checks for refresh conditions, and compares the decrypted 
    /// cookie value and submitted value for validity.
    /// </summary>
    /// <param name="captchaValue">The Captcha value submitted by the client.</param>
    /// <returns>True if the Captcha value is valid, false otherwise.</returns>
    public bool Validate(string captchaValue)
    {
        var coockieValue = _tokenSerializer.Deserialize();
        var cachedToken = _captchaTokenCache.Retrieve(_tokenCacheKey);
        _token = cachedToken;

        if (TryRefresh())
            return false;

        if (coockieValue != cachedToken.token)
        {
            _counter++;
            return false;
        }

        if (string.IsNullOrWhiteSpace(captchaValue) || string.IsNullOrWhiteSpace(coockieValue) ||
            _captchaTokenEncrypt.Decrypt(coockieValue) != captchaValue)
        {
            _counter++;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Expires the current CAPTCHA token, clearing it from the cache, 
    /// resetting the token serializer, and resetting the internal counter.
    /// This method is typically called when a CAPTCHA challenge is successfully 
    /// solved or when it's time to generate a new CAPTCHA.
    /// </summary>
    public void Expire()
    {
        _captchaTokenCache.Clear(_tokenCacheKey);
        _tokenSerializer.Clear();
        _counter.Reset();
    }

    /// <summary>
    /// This private helper method checks if the Captcha needs refresh based on attempt count or expiration time.
    /// It resets the counter and clears the cached and cookie values if refresh is required.
    /// </summary>
    /// <returns>True if the Captcha needs refresh, false otherwise.</returns>
    private bool TryRefresh()
    {
        if (_counter.Count == _options.MaximumCaptchaAttempts)
        {
            _captchaTokenCache.Clear(_tokenCacheKey);
            _tokenSerializer.Clear();
            _counter.Reset();
            return true;
        }

        var captchaLifeTime = _token.creationDateTime.Add(_options.CaptchaLifeTime);
        if (captchaLifeTime < DateTime.Now)
        {
            _captchaTokenCache.Clear(_tokenCacheKey);
            _tokenSerializer.Clear();
            _counter.Reset();
            return true;
        }

        return false;
    }
}
