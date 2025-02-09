namespace NSCaptcha;

/// <summary>
/// Defines the interface for drawing text onto an image.
/// Implementations of this interface provide a way to generate image data (typically a byte array) 
/// representing an image with the specified text rendered on it.
/// </summary>
public interface IImageDrawer
{
    /// <summary>
    /// Draws the specified text onto an image and returns the image data as a byte array.
    /// </summary>
    /// <param name="text">The text to draw on the image.</param>
    /// <returns>A byte array representing the image data (e.g., PNG, JPEG, GIF format).</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when the provided text is null.</exception>
    /// <exception cref="System.ArgumentException">Thrown when the provided text is empty or contains only whitespace.</exception>
    /// <exception cref="CaptchaException">Thrown when an error occurs during image creation or text rendering 
    /// (e.g., font not found, graphics context failure, image format error).</exception>
    byte[] DrawText(string text);
}