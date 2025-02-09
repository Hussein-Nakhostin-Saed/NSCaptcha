using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// This interface defines the contract for a service that handles Captcha generation and validation.
/// </summary>
public interface ICaptchaService
{
    /// <summary>
    /// Generates a new Captcha challenge.
    /// </summary>
    /// <returns>A new Captcha object containing the Captcha image and associated data.</returns>
    Captcha Create();

    /// <summary>
    /// Validates the provided Captcha value against the current challenge.
    /// </summary>
    /// <param name="captchaValue">The Captcha value entered by the user.</param>
    /// <returns>True if the Captcha value is valid, false otherwise.</returns>
    bool Validate(string captchaValue);
}
