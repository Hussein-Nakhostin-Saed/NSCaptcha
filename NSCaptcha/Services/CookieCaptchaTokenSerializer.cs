using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// Implements the <see cref="ICaptchaTokenSerializer"/> interface to serialize and deserialize Captcha tokens using HTTP cookies.
/// </summary>
public class CookieCaptchaTokenSerializer : ICaptchaTokenSerializer
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly CaptchaCookieOptions _cookieOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieCaptchaTokenSerializer"/> class.
    /// </summary>
    /// <param name="contextAccessor">The <see cref="IHttpContextAccessor"/> used to access the HTTP context.</param>
    /// <param name="cookieOptions">The <see cref="CaptchaCookieOptions"/> containing the cookie configuration.</param>
    public CookieCaptchaTokenSerializer(IHttpContextAccessor contextAccessor, CaptchaCookieOptions cookieOptions)
    {
        _contextAccessor = contextAccessor;
        _cookieOptions = cookieOptions;
    }

    /// <summary>
    /// Serializes the Captcha token and stores it in an HTTP cookie.
    /// </summary>
    /// <param name="token">The Captcha token to serialize.</param>
    /// <exception cref="CaptchaException">Thrown when the provided token is null or empty.</exception>
    public void Serialize(string token)
    {
        if (string.IsNullOrEmpty(token.Trim()))
            throw new CaptchaException("Invalid Token");

        _contextAccessor.HttpContext.Response.Cookies.Append(_cookieOptions.Name, token, _cookieOptions);
    }

    /// <summary>
    /// Deserializes the Captcha token from the HTTP cookie.
    /// </summary>
    /// <returns>The deserialized Captcha token.</returns>
    /// <exception cref="CaptchaException">Thrown when the cookie with the specified name does not exist.</exception>
    public string Deserialize()
    {
        if (!_contextAccessor.HttpContext.Request.Cookies.Any())
            throw new CaptchaException("Cookie value does not exist");

        var fromCookieCaptcha = _contextAccessor.HttpContext.Request.Cookies[_cookieOptions.Name];
        if (string.IsNullOrEmpty(fromCookieCaptcha))
            throw new CaptchaException("Cookie value does not exist");

        return fromCookieCaptcha;
    }

    /// <summary>
    /// Clears the Captcha token from the HTTP cookie.
    /// </summary>
    /// <exception cref="CaptchaException">Thrown when the cookie with the specified name does not exist.</exception>
    public void Clear()
    {
        var fromCookieCaptcha = _contextAccessor.HttpContext.Request.Cookies[_cookieOptions.Name];
        if (string.IsNullOrEmpty(fromCookieCaptcha.Trim()))
            throw new CaptchaException("Cookie Key does not exist");

        // Setting the value to an empty string and then deleting ensures the cookie is properly removed
        // across different browsers and configurations.
        _contextAccessor.HttpContext.Response.Cookies.Append(_cookieOptions.Name, "");
        _contextAccessor.HttpContext.Response.Cookies.Delete(_cookieOptions.Name);
    }
}