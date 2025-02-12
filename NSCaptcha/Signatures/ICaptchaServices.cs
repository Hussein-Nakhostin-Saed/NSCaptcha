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

    /// <summary>
    /// Expires the current CAPTCHA token, clearing it from the cache, 
    /// resetting the token serializer, and resetting the internal counter.
    /// This method is typically called when a CAPTCHA challenge is successfully 
    /// solved or when it's time to generate a new CAPTCHA.
    /// </summary>
    void Expire();
}
