using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSCaptcha.Utilities;

namespace NSCaptcha;

/// <summary>
/// Provides extension methods for adding captcha services to the dependency injection container.
/// </summary>
public static class CaptchaExtentions
{
    /// <summary>
    /// Adds captcha services to the service collection with default options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>An <see cref="ICaptchaBuilder"/> instance that allows further customization of the captcha.</returns>
    public static ICaptchaBuilder AddCaptcha(this IServiceCollection services)
    {
        return ConfigureCommonServices(services, new CaptchaOptions(), new CaptchaCookieOptions()); // Calls the overload with default options.
    }

    /// <summary>
    /// Adds captcha services to the service collection with custom options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="options">An action to configure the <see cref="CaptchaOptions"/>.</param>
    /// <returns>An <see cref="ICaptchaBuilder"/> instance that allows further customization of the captcha.</returns>
    public static ICaptchaBuilder AddCaptcha(this IServiceCollection services, Action<CaptchaOptions> options)
    {
        var captchaOptions = new CaptchaOptions();
        var cookieOptions = new CaptchaCookieOptions();
        options(captchaOptions); // Applies the custom options.
        return ConfigureCommonServices(services, captchaOptions, cookieOptions); // Configures the common services.
    }

    /// <summary>
    /// Adds captcha services to the service collection with custom options and cookie options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="options">An action to configure the <see cref="CaptchaOptions"/> and <see cref="CaptchaCookieOptions"/>.</param>
    /// <returns>An <see cref="ICaptchaBuilder"/> instance that allows further customization of the captcha.</returns>
    public static ICaptchaBuilder AddCaptcha(this IServiceCollection services, Action<CaptchaOptions, CaptchaCookieOptions> options)
    {
        var captchaOptions = new CaptchaOptions();
        var cookieOptions = new CaptchaCookieOptions();
        options(captchaOptions, cookieOptions); // Applies the custom options.

        return ConfigureCommonServices(services, captchaOptions, cookieOptions); // Configures the common services.
    }

    /// <summary>
    /// Configures the common captcha services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="captchaOptions">The captcha options.</param>
    /// <returns>A <see cref="CaptchaBuilder"/> instance.</returns>
    private static CaptchaBuilder ConfigureCommonServices(IServiceCollection services, CaptchaOptions captchaOptions, CaptchaCookieOptions cookieOptions)
    {
        captchaOptions.Initial(); // Initializes the captcha options.
        var internalCaptchaOptions = Map(captchaOptions); // Maps the CaptchaOptions to InternalCaptchaOptions.

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Registers the HttpContextAccessor.
        services.AddSingleton(internalCaptchaOptions); // Registers the InternalCaptchaOptions as a singleton.
        services.AddSingleton(cookieOptions); // Registers the cookie options as a singleton.

        services.AddScoped<IImageDrawer, ImageDrawService>(); // Registers the ImageDrawService as scoped.
        services.AddScoped<ValidateCaptchaAttribute>(); // Registers the ValidateCaptchaAttribute as scoped.
        var captchaBuilder = new CaptchaBuilder(services); // Creates a new CaptchaBuilder.
        captchaBuilder.InitialDefaultServices(); // Initializes the default services.
        services.AddScoped<ICaptchaService, CaptchaService>(); // Registers the CaptchaService as scoped.

        return captchaBuilder;
    }

    /// <summary>
    /// Maps the <see cref="CaptchaOptions"/> to <see cref="InternalCaptchaOptions"/>.
    /// </summary>
    /// <param name="options">The captcha options.</param>
    /// <returns>An <see cref="InternalCaptchaOptions"/> instance.</returns>
    private static InternalCaptchaOptions Map(CaptchaOptions? options)
    {
        options = options ?? new CaptchaOptions(); // Uses default options if options are null.
        // Creates a new InternalCaptchaOptions instance by mapping properties from CaptchaOptions.
        var internalCaptchaOptions = new InternalCaptchaOptions(options.Content.IncludeUpperCaseLetters,
            options.Content.IncludeLowerCaseLetters,
            options.Content.IncludeDigits,
            options.Content.IncludeSymbols,
            options.Content.Length,
            options.Font.FontFamilies,
            options.Style.TextColor.Select(x => Color.FromRgba(x.R, x.G, x.B, x.A)).ToArray(),
            options.Style.LinesColors.Select(x => Color.FromRgba(x.R, x.G, x.B, x.A)).ToArray(),
            options.Style.MinLineThickness,
            options.Style.MaxLineThickness,
            options.Style.Width,
            options.Style.Height,
            options.Noise.NoiseRate,
            options.Style.NoiseColors.Select(x => Color.FromRgba(x.R, x.G, x.B, x.A)).ToArray(),
            options.Font.FontSize,
            options.Font.FontStyle.ParseToNSCaptcha(),
            options.EncoderType,
            options.Noise.LineCount,
            options.Noise.MaxRotationDegrees,
            options.Style.BackgroundColors.Select(x => Color.FromRgba(x.R, x.G, x.B, x.A)).ToArray(),
            options.MaximumCaptchaAttempts,
            options.CaptchaLifeTime);
        return internalCaptchaOptions;
    }
}