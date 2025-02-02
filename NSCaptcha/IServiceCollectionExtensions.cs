using Microsoft.Extensions.DependencyInjection;
using NCaptcha.Utilities;

namespace NCaptcha;

public sealed class CaptchaServiceCollectionBuilder
{
    private readonly IServiceCollection _services;
    internal CaptchaServiceCollectionBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public CaptchaServiceCollectionBuilder AddCaptcha(CaptchaOptions? options = null)
    {
        var internalCaptchaOptions = Map(options ?? new CaptchaOptions());

        _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        _services.AddSingleton(internalCaptchaOptions);
        _services.AddScoped<IImageDrawer, ImageDrawService>();
        _services.AddScoped<ValidateCaptchaAttribute>();

        return this;
    }

    public CaptchaServiceCollectionBuilder AddCaptchaTokenSerializer<TSerializer>() where TSerializer : ICaptchaTokenSerializer
    {
        _services.AddScoped(typeof(ICaptchaTokenSerializer), typeof(TSerializer));
        return this;
    }

    public CaptchaServiceCollectionBuilder AddCaptchaTokenEncryptionService<TEncryptionService>() where TEncryptionService : ICaptchaTokenProtector
    {
        _services.AddScoped(typeof(ICaptchaTokenProtector), typeof(TEncryptionService));
        return this;
    }

    public CaptchaServiceCollectionBuilder AddCaptchaTokenCacheService<TCaptchaTokenCacheService>() where TCaptchaTokenCacheService : ICaptchaTokenCache
    {
        _services.AddScoped(typeof(ICaptchaTokenCache), typeof(TCaptchaTokenCacheService));
        return this;
    }

    private static InternalCaptchaOptions Map(CaptchaOptions? options)
    {
        options = options ?? new CaptchaOptions();
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
                                                                options.Font.FontStyle.ParseToNCaptcha(),
                                                                options.EncoderType,
                                                                options.Noise.LineCount,
                                                                options.Noise.MaxRotationDegrees,
                                                                options.Style.BackgroundColors.Select(x => Color.FromRgba(x.R, x.G, x.B, x.A)).ToArray(),
                                                                options.MaximumCaptchaAttempts,
                                                                options.CaptchaExpirationLifeTime);
        return internalCaptchaOptions;
    }

    public CaptchaServiceCollectionBuilder Build()
    {
        _services.AddScoped<ICaptchaServices, CaptchaServices>();
        return this;
    }
}

public static class CaptchaServiceCollection
{
    public static CaptchaServiceCollectionBuilder CreateBuilder(this IServiceCollection services) => new CaptchaServiceCollectionBuilder(services);
}
