using System.Security.Cryptography;
using System.Text;

namespace NSCaptcha.Utilities;

/// <summary>
/// A static utility class providing helper methods for generating random strings, hashing strings,
/// generating random floats, and converting between font style enums.
/// </summary>
internal static class Utils
{
    /// <summary>
    /// Creates a random string based on the provided <see cref="RandomStringModel"/> configuration.
    /// </summary>
    /// <param name="model">The <see cref="RandomStringModel"/> instance specifying the character set and length.</param>
    /// <returns>A randomly generated string.</returns>
    public static string CreateRandomStringAsync(RandomStringModel model)
    {
        string captchaFormat = string.Empty; // Initializes an empty string to hold the characters allowed in the CAPTCHA.
        int captchaFormatCount = 0; // Initializes a counter to keep track of the number of available characters.
        string res = ""; // Initializes an empty string to store the resulting random string.

        if (model.IncludeUpperCaseLetters) // Checks if uppercase letters should be included.
        {
            var upperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Defines a string containing all uppercase letters.
            captchaFormat += upperCases; // Appends the uppercase letters to the `captchaFormat` string.
            captchaFormatCount += upperCases.Length; // Adds the number of uppercase letters to the character count.
        }

        if (model.IncludeLowerCaseLetters) // Checks if lowercase letters should be included.
        {
            var lowerCases = "abcdefghijklmnopqrstuvwxyz"; // Defines a string containing all lowercase letters.
            captchaFormat += lowerCases; // Appends the lowercase letters to the `captchaFormat` string.
            captchaFormatCount += lowerCases.Length; // Adds the number of lowercase letters to the character count.
        }

        if (model.IncludeDigits) // Checks if digits should be included.
        {
            var digits = "0123456789"; // Defines a string containing all digits.
            captchaFormat += digits; // Appends the digits to the `captchaFormat` string.
            captchaFormatCount += digits.Length; // Adds the number of digits to the character count.
        }

        if (model.IncludeSymbols) // Checks if symbols should be included.
        {
            var symbols = @"!#@%&*/\()_+=-?"; // Defines a string containing the allowed symbols.
            captchaFormat += symbols; // Appends the symbols to the `captchaFormat` string.
            captchaFormatCount += symbols.Length; // Adds the number of symbols to the character count.
        }

        for (int i = 1; i <= model.Length; i++) // Loops `model.Length` times to generate the random string.
        {
            var ABCi = new Random().Next(1, captchaFormatCount); // Generates a random integer between 1 (inclusive) and `captchaFormatCount` (exclusive).  **POTENTIAL ISSUE: Random.Next(int, int) is exclusive of the max value.  This could cause an index out of bounds exception. Should be Random().Next(0, captchaFormatCount);
            string strABCi = captchaFormat.Substring(ABCi, 1); // Extracts the character at the randomly generated index from `captchaFormat`.
            res += strABCi; // Appends the extracted character to the `res` string.
        }

        return res; // Returns the generated random string.
    }

    /// <summary>
    /// Computes the SHA256 hash of the given string.
    /// </summary>
    /// <param name="text">The string to hash.</param>
    /// <returns>The SHA256 hash of the string, represented as a hexadecimal string.</returns>
    public static string HashString(string text)
    {
        string resultAsHash = string.Empty; // Initializes an empty string to store the resulting hash value.

        using (SHA256 sha256 = SHA256.Create()) // Creates a SHA256 hashing algorithm object using a 'using' statement for proper disposal.  The 'using' statement ensures that the SHA256 object is disposed of correctly after it's used, even if exceptions occur.
        {
            byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(text)); // Computes the SHA256 hash of the input 'text' (converted to a byte array using UTF8 encoding) and stores the hash value in a byte array.
            resultAsHash = Convert.ToHexString(hashValue1); // Converts the byte array containing the hash value to a hexadecimal string representation and stores it in 'resultAsHash'.
        } // The SHA256 object is automatically disposed of here.

        return resultAsHash; // Returns the hexadecimal string representation of the SHA256 hash.
    }

    /// <summary>
    /// Generates a random float within the specified range.
    /// </summary>
    /// <param name="min">The minimum value of the range (inclusive).</param>
    /// <param name="max">The maximum value of the range (inclusive).</param>
    /// <returns>A random float within the specified range.</returns>
    public static float GenerateNextFloat(double min = -3.40282347E+38, double max = 3.40282347E+38)
    {
        Random random = new Random(); // Creates a new instance of the Random number generator.  Note: For better performance, especially if calling this method frequently, consider creating a single static instance of Random and reusing it.
        double range = max - min; // Calculates the range between the maximum and minimum values.
        double sample = random.NextDouble(); // Generates a random double-precision floating-point number between 0.0 (inclusive) and 1.0 (exclusive).
        double scaled = sample * range + min; // Scales the random sample to the desired range by multiplying by the range and adding the minimum value.  This maps the 0-1 range to the [min, max] range.
        float result = (float)scaled; // Casts the scaled double value to a single-precision float.  This might result in some loss of precision.
        return result; // Returns the generated random float.
    }

    /// <summary>
    /// Converts a <see cref="FontStyle"/> enum value to its corresponding <see cref="SixLabors.Fonts.FontStyle"/> value.
    /// </summary>
    /// <param name="fontStyle">The <see cref="FontStyle"/> value to convert.</param>
    /// <returns>The equivalent <see cref="SixLabors.Fonts.FontStyle"/> value.</returns>
    public static SixLabors.Fonts.FontStyle ParseToNSCaptcha(this FontStyle fontStyle) => (SixLabors.Fonts.FontStyle)Enum.ToObject(typeof(SixLabors.Fonts.FontStyle), fontStyle);
}
