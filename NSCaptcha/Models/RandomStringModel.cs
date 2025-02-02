namespace NCaptcha;

internal class RandomStringModel
{
    public bool IncludeUpperCaseLetters { get; set; }
    public bool IncludeLowerCaseLetters { get; set; }
    public bool IncludeDigits { get; set; }
    public bool IncludeSymbols { get; set; }
    public int Length { get; set; }

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
