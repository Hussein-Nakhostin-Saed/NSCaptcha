using NCaptcha.Utilities;

namespace NCaptcha;

public class CaptchaServices : ICaptchaServices
{
    private readonly InternalCaptchaOptions _options;
    private Counter _counter = Counter.Instance;
    private readonly IImageDrawer _imageDraw;
    private readonly ICaptchaTokenSerializer _tokenSerializer;
    private readonly ICaptchaTokenProtector _captchaTokenEncrypt;
    private readonly ICaptchaTokenCache _captchaTokenCache;
    private string _tokenCacheKey = "captchaData";
    private Token _token;

    public CaptchaServices(IImageDrawer imageDrawer,
                             ICaptchaTokenSerializer tokenSerializer,
                             ICaptchaTokenProtector captchaTokenEncrypt,
                             InternalCaptchaOptions options,
                             ICaptchaTokenCache captchaTokenCache)
    {
        _options = options;
        _imageDraw = imageDrawer;
        _tokenSerializer = tokenSerializer;
        _captchaTokenCache = captchaTokenCache;
        _captchaTokenEncrypt = captchaTokenEncrypt;
    }

    public Captcha Create()
    {
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

    private bool TryRefresh()
    {
        if (_counter.Count == _options.MaximumCaptchaAttempts)
        {
            _captchaTokenCache.Clear(_tokenCacheKey);
            _tokenSerializer.Clear();
            _counter.Reset();
            return true;
        }

        var captchaLifeTime = _token.creationDateTime.Add(_options.CaptchaExpirationLifeTime);
        if(captchaLifeTime < DateTime.Now)
        {
            _captchaTokenCache.Clear(_tokenCacheKey);
            _tokenSerializer.Clear();
            _counter.Reset();
            return true;
        }

        return false;
    }
}
