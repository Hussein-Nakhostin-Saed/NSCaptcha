using Microsoft.Extensions.DependencyInjection;

namespace NSCaptcha;

/// <summary>
/// A sealed class that implements the <see cref="ICaptchaBuilder"/> interface to configure and build 
/// the Captcha generation system. This class uses an <see cref="IServiceCollection"/> to register 
/// the necessary dependencies for the Captcha system.
/// </summary>
internal sealed class CaptchaBuilder : ICaptchaBuilder
{
    private readonly IServiceCollection _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptchaBuilder"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to register services with.</param>
    internal CaptchaBuilder(IServiceCollection services)
    {
        _services = services;
    }

    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenSerializer"/> with the service collection.
    /// The registered service is keyed with "TokenSerializer".
    /// </summary>
    /// <typeparam name="TSerializer">The type of the <see cref="ICaptchaTokenSerializer"/> implementation.</typeparam>
    /// <returns>The current <see cref="CaptchaBuilder"/> instance for chaining method calls.</returns>
    public ICaptchaBuilder AddCaptchaTokenSerializer<TSerializer>() where TSerializer : ICaptchaTokenSerializer
    {
        _services.AddKeyedScoped(typeof(ICaptchaTokenSerializer), "TokenSerializer", typeof(TSerializer));
        return this;
    }

    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenProtector"/> with the service collection.
    /// The registered service is keyed with "DataProtection".
    /// </summary>
    /// <typeparam name="TEncryptionService">The type of the <see cref="ICaptchaTokenProtector"/> implementation.</typeparam>
    /// <returns>The current <see cref="CaptchaBuilder"/> instance for chaining method calls.</returns>
    public ICaptchaBuilder AddCaptchaTokenEncryptionService<TEncryptionService>() where TEncryptionService : ICaptchaTokenProtector
    {
        _services.AddKeyedScoped(typeof(ICaptchaTokenProtector), "DataProtection", typeof(TEncryptionService));
        return this;
    }

    /// <summary>
    /// Registers a custom implementation of <see cref="ICaptchaTokenCache"/> with the service collection.
    /// The registered service is keyed with "TokenCache".
    /// </summary>
    /// <typeparam name="TCaptchaTokenCacheService">The type of the <see cref="ICaptchaTokenCache"/> implementation.</typeparam>
    /// <returns>The current <see cref="CaptchaBuilder"/> instance for chaining method calls.</returns>
    public ICaptchaBuilder AddCaptchaTokenCacheService<TCaptchaTokenCacheService>() where TCaptchaTokenCacheService : ICaptchaTokenCache
    {
        _services.AddKeyedScoped(typeof(ICaptchaTokenCache), "TokenCache", typeof(TCaptchaTokenCacheService));
        return this;
    }

    /// <summary>
    /// Registers default implementations for the essential Captcha services if no custom implementations have been registered.
    /// This method is called internally to ensure the Captcha system has the necessary components.
    /// </summary>
    internal void InitialDefaultServices()
    {
        _services.AddKeyedScoped<ICaptchaTokenSerializer, CookieCaptchaTokenSerializer>("TokenSerializer");
        _services.AddKeyedScoped<ICaptchaTokenCache, InMemoryCaptchaTokenCacheService>("TokenCache");
        _services.AddKeyedScoped<ICaptchaTokenProtector, DefaultDataProtectionService>("DataProtection");
    }
}