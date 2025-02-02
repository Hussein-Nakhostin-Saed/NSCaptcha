namespace NCaptcha;

public interface ICaptchaServices
{
    Captcha Create();
    bool Validate(string captchaValue);
}
