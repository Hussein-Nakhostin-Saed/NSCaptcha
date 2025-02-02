namespace NCaptcha;

/// <summary>
/// Represents a Captcha response containing the image data, captcha value, and a unique token.
/// </summary>
public record class Captcha(byte[] Image, string Value, string Token);
public record class Token(string token, DateTime creationDateTime);
