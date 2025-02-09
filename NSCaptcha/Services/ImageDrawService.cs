using NSCaptcha.Utilities;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace NSCaptcha;

/// <summary>
/// Initializes a new instance of the <see cref="ImageDrawService"/> class.
/// </summary>
/// <param name="options">The captcha options.</param>
internal class ImageDrawService : IImageDrawer
{
    private readonly InternalCaptchaOptions _options; // Options for configuring the captcha image generation.

    // Constructor that injects the captcha options.
    public ImageDrawService(InternalCaptchaOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Draws the captcha text onto an image and returns the image as a byte array.
    /// </summary>
    /// <param name="text">The captcha text.</param>
    /// <returns>A byte array representing the captcha image.</returns>
    public byte[] DrawText(string text)
    {
        byte[] result;

        // Use ImageSharp to create and manipulate the image.
        using (var imgText = new Image<Rgba32>(_options.Width, _options.Height))
        {
            float position = 0; // Tracks the horizontal position of the text.
            Random random = new Random(); // Random number generator for various effects.
            byte startWith = (byte)random.Next(5, 10); // Starting offset for the text.
            imgText.Mutate(ctx => ctx.BackgroundColor(Color.Transparent)); // Set background to transparent.

            Font font = null!; // Stores the font used for the text.

            // Iterate through the available font families and try to create a font.
            foreach (var fontName in _options.FontFamilies)
            {
                try
                {
                    font = SystemFonts.CreateFont(fontName, _options.FontSize, _options.FontStyle); // Create the font.
                    break; // Exit the loop if a font is successfully created.
                }
                catch (Exception ex)
                {
                    // If font creation fails, continue to the next font family.
                    continue;
                }
            }

            // Throw an exception if no valid font could be created.
            if (font == null)
                throw new Exception("Invalid Captcha Font");

            // Draw each character of the text onto the image.
            foreach (char c in text)
            {
                var location = new PointF(startWith + position, random.Next(6, 13)); // Random vertical offset for each character.
                imgText.Mutate(ctx => ctx.DrawText(c.ToString(), font, _options.TextColor[random.Next(0, _options.TextColor.Length)], location)); // Draw the character.
                position += TextMeasurer.MeasureSize(c.ToString(), new TextOptions(font)).Width; // Update the horizontal position.
            }

            AffineTransformBuilder rotation = getRotation(); // Get a random rotation transform.
            imgText.Mutate(ctx => ctx.Transform(rotation)); // Apply the rotation to the text.

            // Create the final image with background and distortions.
            ushort size = (ushort)TextMeasurer.MeasureSize(text, new TextOptions(font)).Width; // Measure the width of the text.
            var img = new Image<Rgba32>(size + 10 + 5, _options.Height); // Create the image with some padding.
            img.Mutate(ctx => ctx.BackgroundColor(_options.BackgroundColors[random.Next(0, _options.BackgroundColors.Length)])); // Set a random background color.


            // Draw random lines on the image.
            Parallel.For(0, _options.LineCount, i =>
            {
                int x0 = random.Next(0, random.Next(0, 30));
                int y0 = random.Next(10, img.Height);
                int x1 = random.Next(img.Width - random.Next(0, (int)(img.Width * 0.25)), img.Width);
                int y1 = random.Next(0, img.Height);
                img.Mutate(ctx =>
                            ctx.DrawLine(_options.DrawLinesColor[random.Next(0, _options.DrawLinesColor.Length)],
                                            Utils.GenerateNextFloat(_options.MinLineThickness, _options.MaxLineThickness),
                                            new PointF[] { new PointF(x0, y0), new PointF(x1, y1) })
                        );
            });

            img.Mutate(ctx => ctx.DrawImage(imgText, 0.80f)); // Draw the text image onto the main image with some opacity.

            // Add random noise to the image.
            Parallel.For(0, _options.NoiseRate, i =>
            {
                int x0 = random.Next(0, img.Width);
                int y0 = random.Next(0, img.Height);
                img.Mutate(
                        ctx => ctx
                            .DrawLine(_options.NoiseRateColor[random.Next(0, _options.NoiseRateColor.Length)],
                            Utils.GenerateNextFloat(0.5, 1.5), new PointF[] { new Vector2(x0, y0), new Vector2(x0, y0) })
                    );
            });

            img.Mutate(x =>
            {
                x.Resize(_options.Width, _options.Height); // Resize the image to the specified dimensions.
            });

            IImageEncoder encoder; // Image encoder to use.

            // Choose the encoder based on the specified type.
            if (_options.EncoderType == EncoderTypes.Png)
                encoder = new PngEncoder();
            else
                encoder = new JpegEncoder();

            // Save the image to a memory stream and convert it to a byte array.
            using (var ms = new MemoryStream())
            {
                img.Save(ms, encoder);
                result = ms.ToArray();
            }
        }

        return result;
    }

    // Generates a random rotation transform.
    private AffineTransformBuilder getRotation()
    {
        Random random = new Random();
        var builder = new AffineTransformBuilder();
        var width = random.Next(10, _options.Width);
        var height = random.Next(10, _options.Height);
        var pointF = new PointF(width, height); // Rotation center.
        var rotationDegrees = random.Next(0, _options.MaxRotationDegrees); // Random rotation angle.
        var result = builder.PrependRotationDegrees(rotationDegrees, pointF); // Create the rotation transform.
        return result;
    }
}