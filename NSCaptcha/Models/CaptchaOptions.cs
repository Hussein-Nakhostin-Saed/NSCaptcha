using NSCaptcha;

namespace NSCaptcha;

/// <summary>
/// Represents the options for configuring captcha generation.
/// </summary>
public class CaptchaOptions
{
    /// <summary>
    /// Gets the options for configuring the captcha content (characters).
    /// </summary>
    public CaptchaContent Content { get; }
    /// <summary>
    /// Gets the options for configuring the captcha font.
    /// </summary>
    public CaptchaFont Font { get; }
    /// <summary>
    /// Gets the options for configuring the captcha style.
    /// </summary>
    public CaptchaStyle Style { get; }
    /// <summary>
    /// Gets the options for configuring the captcha noise.
    /// </summary>
    public CaptchaNoise Noise { get; }
    /// <summary>
    /// Gets or sets the image encoder type to use (PNG or JPEG).
    /// </summary>
    public EncoderTypes EncoderType { get; set; }
    /// <summary>
    /// Gets or sets the maximum number of captcha attempts allowed.
    /// </summary>
    public int MaximumCaptchaAttempts { get; set; }
    /// <summary>
    /// Gets or sets the lifetime of the captcha.
    /// </summary>
    public TimeSpan CaptchaLifeTime { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptchaOptions"/> class.
    /// </summary>
    public CaptchaOptions()
    {
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Style = new CaptchaStyle();
        Noise = new CaptchaNoise();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 5;
        CaptchaLifeTime = new TimeSpan(0, 10, 0);
    }

    /// <summary>
    /// Initializes the nested configuration objects.
    /// </summary>
    internal void Initial()
    {
        Content.SetValues();
        Font.SetValues();
        Style.SetValues();
        Noise.SetValues();
    }
}

/// <summary>
/// Represents the options for configuring the captcha content (characters).
/// </summary>
public class CaptchaContent
{
    private bool _includeUpperCaseLetters = true;
    private bool _includeLowerCaseLetters = false;
    private bool _includeDigits = true;
    private bool _includeSymbols = false;
    private int _length = 6;

    /// <summary>
    /// Gets whether to include uppercase letters.
    /// </summary>
    public bool IncludeUpperCaseLetters { get; private set; }
    /// <summary>
    /// Gets whether to include lowercase letters.
    /// </summary>
    public bool IncludeLowerCaseLetters { get; private set; }
    /// <summary>
    /// Gets whether to include digits.
    /// </summary>
    public bool IncludeDigits { get; private set; }
    /// <summary>
    /// Gets whether to include symbols.
    /// </summary>
    public bool IncludeSymbols { get; private set; }
    /// <summary>
    /// Gets the length of the captcha.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Sets the default values for the properties.
    /// </summary>
    internal void SetValues()
    {
        IncludeUpperCaseLetters = _includeUpperCaseLetters;
        IncludeLowerCaseLetters = _includeLowerCaseLetters;
        IncludeDigits = _includeDigits;
        IncludeSymbols = _includeSymbols;
        Length = _length;
    }

    /// <summary>
    /// Configures the captcha to use letters.
    /// </summary>
    /// <param name="useUpperCase">Whether to use uppercase letters.</param>
    /// <param name="uselowerCase">Whether to use lowercase letters.</param>
    /// <returns>The <see cref="CaptchaContent"/> instance for chaining.</returns>
    public CaptchaContent UseLetters(bool useUpperCase, bool uselowerCase)
    {
        _includeUpperCaseLetters = useUpperCase;
        _includeLowerCaseLetters = uselowerCase;

        return this;
    }


    /// <summary>
    /// Configures the captcha to use digits.
    /// </summary>
    /// <returns>The <see cref="CaptchaContent"/> instance for chaining.</returns>
    public CaptchaContent UseDigits()
    {
        _includeDigits = true;
        return this;
    }

    /// <summary>
    /// Configures the captcha to use symbols.
    /// </summary>
    /// <returns>The <see cref="CaptchaContent"/> instance for chaining.</returns>
    public CaptchaContent UseSymbols()
    {
        _includeSymbols = true;
        return this;
    }

    /// <summary>
    /// Configures the length of the captcha.
    /// </summary>
    /// <returns>The <see cref="CaptchaContent"/> instance for chaining.</returns>
    public CaptchaContent WithLength(int length)
    {
        _length = length;
        return this;
    }
}


/// <summary>
/// Represents the options for configuring the captcha font.
/// </summary>
public class CaptchaFont
{
    private FontStyle _fontStyle = FontStyle.Regular;
    private float _fontSize = 29;
    private string[] _fontFamilies = ["IRANSansWebFaNum", "Arial", "DejaVu Sans", "Verdana", "Liberation Sans", "Hack-Regular"];

    /// <summary>
    /// Gets the font style.
    /// </summary>
    public FontStyle FontStyle { get; private set; }

    /// <summary>
    /// Gets the font size.
    /// </summary>
    public float FontSize { get; private set; }

    /// <summary>
    /// Gets the font families.
    /// </summary>
    public string[] FontFamilies { get; private set; }

    /// <summary>
    /// Sets the default values for the properties.
    /// </summary>
    internal void SetValues()
    {
        FontStyle = _fontStyle;
        FontSize = _fontSize;
        FontFamilies = _fontFamilies;
    }

    /// <summary>
    /// Sets the font style.
    /// </summary>
    /// <param name="style">The font style.</param>
    /// <returns>The <see cref="CaptchaFont"/> instance for chaining.</returns>
    public CaptchaFont AddStyle(FontStyle style)
    {
        _fontStyle = style;
        return this;
    }

    /// <summary>
    /// Sets the font size.
    /// </summary>
    /// <param name="size">The font size.</param>
    /// <returns>The <see cref="CaptchaFont"/> instance for chaining.</returns>
    public CaptchaFont Resize(float size)
    {
        _fontSize = size;
        return this;
    }

    /// <summary>
    /// Sets the font families.
    /// </summary>
    /// <param name="fontFamilies">The font families.</param>
    /// <returns>The <see cref="CaptchaFont"/> instance for chaining.</returns>
    public CaptchaFont AddFontFamily(params string[] fontFamilies)
    {
        _fontFamilies = fontFamilies;
        return this;
    }
}

/// <summary>
/// Represents the options for configuring the captcha noise.
/// </summary>
public class CaptchaNoise
{
    private ushort _noiseRate = 800;
    private byte _lineCount = 5;
    private byte _maxRotationDegrees = 5;

    /// <summary>
    /// Gets the noise rate.
    /// </summary>
    public ushort NoiseRate { get; private set; }

    /// <summary>
    /// Gets the number of lines.
    /// </summary>
    public byte LineCount { get; private set; }

    /// <summary>
    /// Gets the maximum rotation degrees.
    /// </summary>
    public byte MaxRotationDegrees { get; private set; }

    /// <summary>
    /// Sets the default values for the properties.
    /// </summary>
    internal void SetValues()
    {
        NoiseRate = _noiseRate;
        LineCount = _lineCount;
        MaxRotationDegrees = _maxRotationDegrees;
    }

    /// <summary>
    /// Sets a fixed noise rate for the Captcha image.
    /// </summary>
    /// <param name="rate">The noise rate, a value between 0 and a maximum (typically related to image size/complexity). 
    /// Higher values mean more noise.</param>
    /// <returns>The current CaptchaNoise instance for chaining method calls (builder pattern).</returns>
    public CaptchaNoise FixRate(ushort rate)
    {
        _noiseRate = rate;
        return this;
    }

    /// <summary>
    /// Sets a fixed number of lines to be drawn on the Captcha image.
    /// </summary>
    /// <param name="count">The number of lines to add to the image.</param>
    /// <returns>The current CaptchaNoise instance for chaining method calls (builder pattern).</returns>
    public CaptchaNoise FixLines(byte count)
    {
        _lineCount = count;
        return this;
    }

    /// <summary>
    /// Sets the maximum degree of rotation that characters in the Captcha image can be rotated.
    /// </summary>
    /// <param name="degree">The maximum degree of rotation (clockwise or counter-clockwise).  
    /// For example, a value of 15 allows rotations between -15 and +15 degrees.</param>
    /// <returns>The current CaptchaNoise instance for chaining method calls (builder pattern).</returns>
    public CaptchaNoise MaxRotate(byte degree)
    {
        _maxRotationDegrees = degree;
        return this;
    }
}

public class CaptchaStyle
{
    private ushort _width = 180;
    private ushort _height = 50;
    private System.Drawing.Color[] _textColor = [System.Drawing.Color.Black, System.Drawing.Color.Brown, System.Drawing.Color.Gray];
    private System.Drawing.Color[] _linesColors = [System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green];
    private System.Drawing.Color[] _noiseColors = [System.Drawing.Color.Gray];
    private System.Drawing.Color[] _backgroundColors = [System.Drawing.Color.White];
    private float _minLineThickness = 0.7f;
    private float _maxLineThickness = 2;

    public ushort Width { get; private set; }
    public ushort Height { get; private set; }
    public System.Drawing.Color[] TextColor { get; private set; }
    public System.Drawing.Color[] LinesColors { get; private set; }
    public System.Drawing.Color[] NoiseColors { get; private set; }
    public System.Drawing.Color[] BackgroundColors { get; private set; }
    public float MinLineThickness { get; private set; }
    public float MaxLineThickness { get; private set; }

    internal void SetValues()
    {
        Width = _width;
        Height = _height;
        TextColor = _textColor;
        LinesColors = _linesColors;
        NoiseColors = _noiseColors;
        BackgroundColors = _backgroundColors;
        MinLineThickness = _minLineThickness;
        MaxLineThickness = _maxLineThickness;
    }

    public CaptchaStyle FixDimensions(ushort width, ushort height)
    {
        _width = width;
        _height = height;
        return this;
    }

    public CaptchaStyle FixNoiseLinesDiameter(float minLineThickness, float maxLineThickness)
    {
        _minLineThickness = minLineThickness;
        _maxLineThickness = maxLineThickness;
        return this;
    }

    public CaptchaStyle AddTextColors(params System.Drawing.Color[] colors)
    {
        _textColor = colors;
        return this;
    }

    public CaptchaStyle AddNoiseLinesColors(params System.Drawing.Color[] colors)
    {
        _linesColors = colors;
        return this;
    }

    public CaptchaStyle AddNoiseDotsColors(params System.Drawing.Color[] colors)
    {
        _noiseColors = colors;
        return this;
    }

    public CaptchaStyle AddBackgroundColors(params System.Drawing.Color[] colors)
    {
        _backgroundColors = colors;
        return this;
    }
}


public class InternalCaptchaOptions
{
    public bool IncludeUpperCaseLetters { get; internal init; }
    public bool IncludeLowerCaseLetters { get; internal init; }
    public bool IncludeDigits { get; internal init; }
    public bool IncludeSymbols { get; internal init; }
    public int Length { get; internal init; }
    public string[] FontFamilies { get; internal init; }
    public Color[] TextColor { get; internal init; }
    public Color[] DrawLinesColor { get; internal init; }
    public float MinLineThickness { get; internal init; }
    public float MaxLineThickness { get; internal init; }
    public ushort Width { get; internal init; }
    public ushort Height { get; internal init; }
    public ushort NoiseRate { get; internal init; }
    public Color[] NoiseRateColor { get; internal init; }
    public float FontSize { get; internal init; }
    public SixLabors.Fonts.FontStyle FontStyle { get; internal init; }
    public EncoderTypes EncoderType { get; internal init; }
    public byte LineCount { get; internal init; }
    public byte MaxRotationDegrees { get; internal init; }
    public Color[] BackgroundColors { get; internal init; }
    public int MaximumCaptchaAttempts { get; internal init; }
    public TimeSpan CaptchaLifeTime { get; internal init; }

    internal InternalCaptchaOptions(bool includeUpperCaseLetters,
                                    bool includeLowerCaseLetters,
                                    bool includeDigits,
                                    bool includeSymbols,
                                    int length,
                                    string[] fontFamilies,
                                    Color[] textColor,
                                    Color[] drawLinesColor,
                                    float minLineThickness,
                                    float maxLineThickness,
                                    ushort width,
                                    ushort height,
                                    ushort noiseRate,
                                    Color[] noiseRateColor,
                                    float fontSize,
                                    SixLabors.Fonts.FontStyle fontStyle,
                                    EncoderTypes encoderType,
                                    byte lineCount,
                                    byte maxRotationDegrees,
                                    Color[] backgroundColors,
                                    int maximumCaptchaAttempts,
                                    TimeSpan captchaLifeTime)
    {
        IncludeUpperCaseLetters = includeUpperCaseLetters;
        IncludeLowerCaseLetters = includeLowerCaseLetters;
        IncludeDigits = includeDigits;
        IncludeSymbols = includeSymbols;
        Length = length;
        FontFamilies = fontFamilies;
        TextColor = textColor;
        DrawLinesColor = drawLinesColor;
        MinLineThickness = minLineThickness;
        MaxLineThickness = maxLineThickness;
        Width = width;
        Height = height;
        NoiseRate = noiseRate;
        NoiseRateColor = noiseRateColor;
        FontSize = fontSize;
        FontStyle = fontStyle;
        EncoderType = encoderType;
        LineCount = lineCount;
        MaxRotationDegrees = maxRotationDegrees;
        BackgroundColors = backgroundColors;
        MaximumCaptchaAttempts = maximumCaptchaAttempts;
        CaptchaLifeTime = captchaLifeTime;
    }
}

