![NSCaptcha-Photoroom](https://github.com/user-attachments/assets/b0bd4a7d-fd07-438e-9e59-b17dae8e4eaf)

**A .NET library for easy CAPTCHA integration**

[![NuGet](https://img.shields.io/nuget/v/NSCaptcha.svg)](https://www.nuget.org/packages/NSCaptcha)

**Features**

* **Easy Integration:** Seamlessly integrate CAPTCHA into your ASP.NET, WPF, or any other .NET project.
* **Customizable:** Configure CAPTCHA appearance, difficulty, and behavior to suit your specific needs.
* **High Security:** Robust CAPTCHA generation and validation mechanisms to prevent bots and automated attacks.
* **Cross-Platform:** Compatible with various .NET platforms and operating systems.

## License

MIT

## Installation

1. **Install via NuGet Package Manager:**
   - Open the NuGet Package Manager in Visual Studio.
   - Search for "NSCaptcha" in the package manager.
   - Select the "NSCaptcha" package and click "Install."
   - Accept the license agreement (if applicable).

2. **Install via .NET CLI:**
   - Open a command prompt or terminal window.
   - Navigate to your project's directory.
   - Use the following command:
     
     ```bash
     dotnet add package NSCaptcha
     ```
## Usage

Refer to the documentation: Detailed usage examples and API documentation are available within the package or on the project website.

 **Default Configuration**

```csharp
builder.Services.AddDataProtection().SetApplicationName("<Your App Name>").PersistKeysToFileSystem(new DirectoryInfo(Path.Combine("<File Location In System>")));
builder.Services.AddMemoryCache();

captchaBuilder.AddCaptcha(new CaptchaOptions(new TimeSpan(0, 0, 30)))
    .AddCaptchaTokenEncryptionService<DefaultDataProtectionService>()
    .AddCaptchaTokenSerializer<InCookieCaptchaTokenSerializer>()
    .AddCaptchaTokenCacheService<InMemoryCaptchaTokenCacheService>()
    .Build();
````

**After App Build**
```csharp
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});
````
**Create Captcha**
```csharp
[Route("api/captcha/create")]
[ApiController]
public class CreationController : ControllerBase
{
    private readonly ICaptchaServices _captchaService;
    public CreationController(ICaptchaServices captchaService)
    {
        _captchaService = captchaService;
    }

    [Route("image")]
    [HttpGet]
    public async Task<FileContentResult> GetImage()
    {
        var captcha = _captchaService.Create();

        return new FileContentResult(captcha.Image, "image/png");
    }
}
````
**Validate Captcha**
* Add `[ValidateCaptcha]` attribute top of your validation action method 
```csharp
    [Route("validate")]
    [HttpPost]
    [ValidateCaptcha]
    public async Task Validate([FromBody] CaptchaTest value)
    {
        
    }
````
**Note:**

* Replace `<File Location In System>` with the actual path to store the data protection keys.
* You can use another options instead `PersistKeysToFileSystem` to configure the data protection system to persist keys
* Notice that your property that you send as captcha value must has "CaptchaValue" name.
* This is a basic example. You can customize it further based on your specific requirements and preferences.

## Project Structure

The project follows a well-organized structure:

* **Filters:** Contains filters for validating CAPTCHA responses.
* **Models:** Contains data models used by the library (e.g., `CaptchaOptions`, `RandomStringModel`).
* **Services:** Contains interfaces and implementations of services related to CAPTCHA (e.g., token generation, caching).
* **Signatures:** Contains interfaces for various components of the CAPTCHA system.
* **Utilities:** Contains helper classes and methods.

## Contributing

Contributions are welcome! Please submit pull requests or create issues on the GitHub repository.
