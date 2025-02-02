using Newtonsoft.Json.Linq;

namespace NCaptcha;

public class InCookieCaptchaTokenSerializer : ICaptchaTokenSerializer
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly string CookieKey = "CaptchaKey";
    public InCookieCaptchaTokenSerializer(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void Serialize(string token)
    {
        if (string.IsNullOrEmpty(token.Trim()))
            throw new InvalidCaptchaException("Invalid Token");

        _contextAccessor.HttpContext.Response.Cookies.Append(CookieKey, token);
    }

    public string Deserialize()
    {
        var fromCookieCaptcha = _contextAccessor.HttpContext.Request.Cookies[CookieKey];
        if (string.IsNullOrEmpty(fromCookieCaptcha.Trim()))
            throw new InvalidCaptchaException("Cookie Key does not exist");

        return fromCookieCaptcha;
    }

    public void Clear()
    {
        var fromCookieCaptcha = _contextAccessor.HttpContext.Request.Cookies[CookieKey];
        if (string.IsNullOrEmpty(fromCookieCaptcha.Trim()))
            throw new InvalidCaptchaException("Cookie Key does not exist");

        _contextAccessor.HttpContext.Response.Cookies.Append(CookieKey, "");
        _contextAccessor.HttpContext.Response.Cookies.Delete(CookieKey);
    }
}
