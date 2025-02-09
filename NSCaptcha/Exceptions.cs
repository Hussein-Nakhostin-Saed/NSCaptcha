namespace NSCaptcha;

/// <summary>
/// Represents an exception that is thrown when an error occurs related to Captcha processing.
/// This exception can be used to indicate various issues, such as invalid Captcha values, 
/// problems with token generation, or other Captcha-related errors.
/// </summary>
public class CaptchaException : Exception
{
    /// <summary>
    /// Gets or sets the invalid value that caused the exception (if applicable).
    /// This property can be used to provide additional context about the error.
    /// </summary>
    public string InvalidValue { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptchaException"/> class with a default message.
    /// </summary>
    public CaptchaException() : base("Invalid Captcha") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptchaException"/> class with a specified error message.
    /// </summary>
    /// <param name="text">The message that describes the error.</param>
    public CaptchaException(string text) : base(text) { }

    // You can add additional constructors if needed, for example:

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptchaException"/> class with a 
    /// specified error message and a reference to the inner exception that is the 
    /// cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, 
    /// or a null reference if no inner exception is specified.</param>
    //public CaptchaException(string message, Exception innerException) : base(message, innerException) { }
}