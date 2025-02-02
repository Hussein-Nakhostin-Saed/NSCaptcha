namespace NCaptcha;

public class InvalidCaptchaException : Exception
{
    public string InvalidValue { get; set; }

    public InvalidCaptchaException() : base("Invalid Captcha") { }
    public InvalidCaptchaException(string text) : base(text) { }
}