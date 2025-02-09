namespace NSCaptcha;

/// <summary>
/// Defines the interface for serializing, deserializing, and clearing Captcha tokens.
/// Implementations of this interface handle the storage and retrieval of Captcha tokens, 
/// allowing for different storage mechanisms (e.g., cookies, session, database).
/// </summary>
public interface ICaptchaTokenSerializer
{
    /// <summary>
    /// Serializes the given Captcha token.
    /// </summary>
    /// <param name="token">The Captcha token to serialize.</param>
    /// <exception cref="CaptchaException">Thrown when an error occurs during serialization 
    /// (e.g., invalid token, storage failure).</exception>
    void Serialize(string token);

    /// <summary>
    /// Deserializes the Captcha token.
    /// </summary>
    /// <returns>The deserialized Captcha token, or null if the token does not exist 
    /// or an error occurs during deserialization.</returns>
    /// <exception cref="CaptchaException">Thrown when an error occurs during deserialization
    /// (e.g., storage access failure, invalid format).</exception>
    string Deserialize();

    /// <summary>
    /// Clears the stored Captcha token.
    /// </summary>
    /// <exception cref="CaptchaException">Thrown when an error occurs while clearing the token
    /// (e.g., storage access failure).</exception>
    void Clear();
}