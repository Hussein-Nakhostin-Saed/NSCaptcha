namespace NSCaptcha;

/// <summary>
/// Represents the configuration options for generating a random string, typically used for Captcha generation.
/// </summary>
internal class RandomStringModel
{
    /// <summary>
    /// Gets or sets a value indicating whether uppercase letters should be included in the random string.
    /// </summary>
    public bool IncludeUpperCaseLetters { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether lowercase letters should be included in the random string.
    /// </summary>
    public bool IncludeLowerCaseLetters { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether digits (0-9) should be included in the random string.
    /// </summary>
    public bool IncludeDigits { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether symbols (e.g., !, @, #, etc.) should be included in the random string.
    /// </summary>
    public bool IncludeSymbols { get; set; }
    /// <summary>
    /// Gets or sets the desired length of the random string.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomStringModel"/> class.
    /// </summary>
    /// <param name="includeUpperCaseLetters">True to include uppercase letters; otherwise, false.</param>
    /// <param name="includeLowerCaseLetters">True to include lowercase letters; otherwise, false.</param>
    /// <param name="includeDigits">True to include digits; otherwise, false.</param>
    /// <param name="includeSymbols">True to include symbols; otherwise, false.</param>
    /// <param name="length">The desired length of the random string.</param>
    public RandomStringModel(
      bool includeUpperCaseLetters,
      bool includeLowerCaseLetters,
      bool includeDigits,
      bool includeSymbols,
      int length)
    {
        IncludeUpperCaseLetters = includeUpperCaseLetters;
        IncludeLowerCaseLetters = includeLowerCaseLetters;
        IncludeDigits = includeDigits;
        IncludeSymbols = includeSymbols;
        Length = length;
    }
}
