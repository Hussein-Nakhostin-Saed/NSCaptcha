using System.Security.Cryptography;
using System.Text;

namespace NCaptcha.Utilities;

internal static class Utils
{
    public static string CreateRandomStringAsync(RandomStringModel model)
    {
        string captchaFormat = string.Empty;
        int captchaFormatCount = 0;
        string res = "";


        if (model.IncludeUpperCaseLetters)
        {
            var upperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            captchaFormat += upperCases;
            captchaFormatCount += upperCases.Length;
        }

        if (model.IncludeLowerCaseLetters)
        {
            var lowerCases = "abcdefghijklmnopqrstuvwxyz";
            captchaFormat += lowerCases;
            captchaFormatCount += lowerCases.Length;
        }

        if (model.IncludeDigits)
        {
            var digits = "0123456789";
            captchaFormat += digits;
            captchaFormatCount += digits.Length;
        }

        if (model.IncludeSymbols)
        {
            var symbols = @"!#@%&*/\()_+=-?";
            captchaFormat += symbols;
            captchaFormatCount += symbols.Length;
        }

        for (int i = 1; i <= model.Length; i++)
        {
            var ABCi = new Random().Next(1, captchaFormatCount);
            string strABCi = captchaFormat.Substring(ABCi, 1);
            res += strABCi;
        }

        return res;
    }

    public static string HashString(string text)
    {
        string resultAsHash = string.Empty;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            resultAsHash = Convert.ToHexString(hashValue1);
        }
        return resultAsHash;
    }

    public static float GenerateNextFloat(double min = -3.40282347E+38, double max = 3.40282347E+38)
    {
        Random random = new Random();
        double range = max - min;
        double sample = random.NextDouble();
        double scaled = sample * range + min;
        float result = (float)scaled;
        return result;
    }

    public static SixLabors.Fonts.FontStyle ParseToNCaptcha(this FontStyle fontStyle) => (SixLabors.Fonts.FontStyle)Enum.ToObject(typeof(SixLabors.Fonts.FontStyle), fontStyle);
}
