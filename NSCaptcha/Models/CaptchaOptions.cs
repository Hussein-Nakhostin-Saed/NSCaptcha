namespace NCaptcha;

public class CaptchaOptions
{
    public CaptchaContent Content { get; }
    public CaptchaFont Font { get; }
    public CaptchaStyle Style { get; }
    public CaptchaNoise Noise { get; }
    public EncoderTypes EncoderType { get; }
    public int MaximumCaptchaAttempts { get; set; }
    public TimeSpan CaptchaExpirationLifeTime { get; init; }

    public CaptchaOptions()
    {
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(CaptchaContent content)
    {
        Content = content;
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(CaptchaFont font)
    {
        Font = font;
        Content = new CaptchaContent();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(CaptchaStyle style)
    {
        Style = style;
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(CaptchaNoise noise)
    {
        Noise = noise;
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(EncoderTypes encoder)
    {
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = encoder;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(int maximumCaptchaAttempts)
    {
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = maximumCaptchaAttempts;
        CaptchaExpirationLifeTime = new TimeSpan(0, 10, 0);
    }

    public CaptchaOptions(TimeSpan captchaExpirationLifeTime)
    {
        Content = new CaptchaContent();
        Font = new CaptchaFont();
        Noise = new CaptchaNoise();
        Style = new CaptchaStyle();
        EncoderType = EncoderTypes.Png;
        MaximumCaptchaAttempts = 4;
        CaptchaExpirationLifeTime = captchaExpirationLifeTime;
    }

    public CaptchaOptions(CaptchaContent content, CaptchaFont font) : this(content)
    {
        Font = font;
    }

    public CaptchaOptions(CaptchaContent content, CaptchaFont font, CaptchaNoise noise) : this(content, font)
    {
        Noise = noise;
    }

    public CaptchaOptions(CaptchaContent content, CaptchaFont font, CaptchaNoise noise, CaptchaStyle style) : this(content, font, noise)
    {
        Style = style;
    }

    public CaptchaOptions(CaptchaContent content, 
                          CaptchaFont font, 
                          CaptchaNoise noise, 
                          CaptchaStyle style, 
                          EncoderTypes encoder) : this(content, font, noise, style)
    {
        EncoderType = encoder;
    }

    public CaptchaOptions(CaptchaContent content,
                          CaptchaFont font, 
                          CaptchaNoise noise, 
                          CaptchaStyle style, 
                          EncoderTypes encoder, 
                          int maximumCaptchaAttempts) : this(content, font, noise, style, encoder)
    {
        MaximumCaptchaAttempts = maximumCaptchaAttempts;
    }

    public CaptchaOptions(CaptchaContent content, 
                          CaptchaFont font, 
                          CaptchaNoise noise, 
                          CaptchaStyle style, 
                          EncoderTypes encoder, 
                          int maximumCaptchaAttempts,
                          TimeSpan captchaExpirationLifeTime) : this(content, font, noise, style, encoder, maximumCaptchaAttempts)
    {
        CaptchaExpirationLifeTime = captchaExpirationLifeTime;
    }
}

public class CaptchaContent
{
    public bool IncludeUpperCaseLetters { get; set; }
    public bool IncludeLowerCaseLetters { get; set; }
    public bool IncludeDigits { get; set; }
    public bool IncludeSymbols { get; set; }
    public int Length { get; set; }

    public CaptchaContent()
    {
        IncludeUpperCaseLetters = true;
        IncludeLowerCaseLetters = false;
        IncludeDigits = true;
        Length = 6;
    }
    public CaptchaContent(bool includeUpperCaseLetters)
    {
        IncludeUpperCaseLetters = includeUpperCaseLetters;
        IncludeLowerCaseLetters = false;
        IncludeDigits = true;
        Length = 6;
    }
    public CaptchaContent(bool includeUpperCaseLetters,
                            bool includeLowerCaseLetters) : this(includeUpperCaseLetters)
    {
        IncludeLowerCaseLetters = includeLowerCaseLetters;
    }
    public CaptchaContent(bool includeUpperCaseLetters,
                            bool includeLowerCaseLetters,
                            bool includeDigits) : this(includeUpperCaseLetters, includeLowerCaseLetters)
    {
        IncludeDigits = includeDigits;
    }
    public CaptchaContent(bool includeUpperCaseLetters,
                            bool includeLowerCaseLetters,
                            bool includeDigits,
                            bool includeSymbols) : this(includeUpperCaseLetters,
                                                        includeLowerCaseLetters,
                                                        includeDigits)
    {
        IncludeSymbols = includeSymbols;
    }
    public CaptchaContent(bool includeUpperCaseLetters,
                            bool includeLowerCaseLetters,
                            bool includeDigits,
                            bool includeSymbols,
                            int length) : this(includeUpperCaseLetters,
                                                        includeLowerCaseLetters,
                                                        includeDigits,
                                                        includeSymbols)
    {
        Length = length;
    }
}

public class CaptchaFont
{
    public FontStyle FontStyle { get; set; }
    public float FontSize { get; set; }
    public string[] FontFamilies { get; set; }

    public CaptchaFont()
    {
        FontStyle = FontStyle.Regular;
        FontSize = 29;
        FontFamilies = ["Arial", "DejaVu Sans", "Verdana", "Liberation Sans", "Hack-Regular", "IRANSansWeb"];
    }
    public CaptchaFont(FontStyle fontStyle)
    {
        FontStyle = fontStyle;
        FontSize = 29;
        FontFamilies = ["Arial", "DejaVu Sans", "Verdana", "Liberation Sans", "Hack-Regular", "IRANSansWeb"];
    }

    public CaptchaFont(FontStyle fontStyle,
                        float fontSize) : this(fontStyle)
    {
        FontSize = fontSize;
    }

    public CaptchaFont(FontStyle fontStyle,
                        float fontSize,
                        params string[] fontFamilies) : this(fontStyle, fontSize)
    {
        FontFamilies = fontFamilies;
    }
}

public class CaptchaNoise
{
    public ushort NoiseRate { get; set; }
    public byte LineCount { get; set; }
    public byte MaxRotationDegrees { get; set; }

    public CaptchaNoise()
    {
        NoiseRate = 800;
        LineCount = 5;
        MaxRotationDegrees = 5;
    }

    public CaptchaNoise(ushort noiseRate)
    {
        NoiseRate = noiseRate;
        LineCount = 5;
        MaxRotationDegrees = 5;
    }

    public CaptchaNoise(ushort noiseRate, byte lineCount) : this(noiseRate)
    {
        LineCount = lineCount;
    }

    public CaptchaNoise(ushort noiseRate, byte lineCount, byte maxRotationDegrees) : this(noiseRate, lineCount)
    {
        MaxRotationDegrees = maxRotationDegrees;
    }
}

public class CaptchaStyle
{
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public System.Drawing.Color[] TextColor { get; set; }
    public System.Drawing.Color[] LinesColors { get; set; }
    public System.Drawing.Color[] NoiseColors { get; set; }
    public System.Drawing.Color[] BackgroundColors { get; set; }
    public float MinLineThickness { get; set; }
    public float MaxLineThickness { get; set; }

    public CaptchaStyle()
    {
        MinLineThickness = 0.7f;
        MaxLineThickness = 2;
        Width = 180;
        Height = 50;
        TextColor = [System.Drawing.Color.Black, System.Drawing.Color.Brown, System.Drawing.Color.Gray];
        LinesColors = [System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green];
        NoiseColors = [System.Drawing.Color.Gray];
        BackgroundColors = [System.Drawing.Color.White];
    }

    public CaptchaStyle(ushort height)
    {
        Height = height;
        Width = 180;
        MinLineThickness = 0.7f;
        MaxLineThickness = 2;
        TextColor = [System.Drawing.Color.Black, System.Drawing.Color.Brown, System.Drawing.Color.Gray];
        LinesColors = [System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green];
        NoiseColors = [System.Drawing.Color.Gray];
        BackgroundColors = [System.Drawing.Color.White];
    }

    public CaptchaStyle(ushort Height,
                          ushort width) : this(Height)
    {
        Width = width;
    }

    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors) : this(height, width)
    {
        BackgroundColors = BackgroundColors;
    }

    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors,
                          System.Drawing.Color[] textColor) : this(height, width, backgroundColors)
    {
        TextColor = textColor;
    }

    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors,
                          System.Drawing.Color[] textColors,
                          System.Drawing.Color[] linesColors) : this(height, width, backgroundColors, textColors)
    {
        LinesColors = LinesColors;
    }
    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors,
                          System.Drawing.Color[] textColors,
                          System.Drawing.Color[] linesColors,
                          System.Drawing.Color[] noiseColors) : this(height, width, backgroundColors, textColors, linesColors)
    {
        NoiseColors = noiseColors;
    }

    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors,
                          System.Drawing.Color[] textColors,
                          System.Drawing.Color[] linesColors,
                          System.Drawing.Color[] noiseColors,
                          float minLineThickness) : this(height, width, backgroundColors, textColors, linesColors, noiseColors)
    {
        MinLineThickness = minLineThickness;
    }

    public CaptchaStyle(ushort height,
                          ushort width,
                          System.Drawing.Color[] backgroundColors,
                          System.Drawing.Color[] textColors,
                          System.Drawing.Color[] linesColors,
                          System.Drawing.Color[] noiseColors,
                          float minLineThickness,
                          float maxLineThickness) : this(height, width, backgroundColors, textColors, linesColors, noiseColors, minLineThickness)
    {
        MaxLineThickness = maxLineThickness;
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
    public TimeSpan CaptchaExpirationLifeTime { get; internal init; }

    internal InternalCaptchaOptions(bool includeUpperCaseLetters,
                                    bool includeLowerCaseLetters,
                                    bool includeDigits,
                                    bool includeSymbols,
                                    int  length,
                                    string[] fontFamilies,
                                    Color[] textColor,
                                    Color[] drawLinesColor,
                                    float  minLineThickness,
                                    float  maxLineThickness,
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
                                    TimeSpan captchaExpirationLifeTime)
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
        CaptchaExpirationLifeTime = captchaExpirationLifeTime;
    }
}

