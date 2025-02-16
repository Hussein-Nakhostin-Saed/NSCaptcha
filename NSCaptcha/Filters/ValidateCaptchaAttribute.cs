using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text.Json;

namespace NSCaptcha;

/// <summary>
/// An action filter attribute that validates a Captcha value submitted with a request.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ValidateCaptchaAttribute : ActionFilterAttribute
{
    private readonly bool _expireAfterValidation;
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidateCaptchaAttribute"/> class.
    /// This attribute is used to validate CAPTCHA responses in ASP.NET MVC actions.
    /// </summary>
    /// <param name="expireAfterValidation">
    /// A boolean value indicating whether the CAPTCHA should expire after successful validation.
    /// If set to <c>true</c> (the default), the CAPTCHA will be considered invalid after it has been successfully verified.
    /// If set to <c>false</c>, the CAPTCHA will remain valid and can be reused (less secure, use with caution).
    /// </param>
    public ValidateCaptchaAttribute(bool expireAfterValidation = true)
    {
        _expireAfterValidation = expireAfterValidation;
    }

    /// <summary>
    /// Asynchronously executes the action filter.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <param name="next">The delegate to execute the next action filter or action.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 1. Get the Captcha service from dependency injection.
        var capatchaServices = context.HttpContext.RequestServices.GetService<ICaptchaService>()!;

        // 2. Fetch the Captcha value from the request.
        var captchaValue = FetchCaptchaValue(context);

        // 3. Validate the Captcha value.
        if (!capatchaServices.Validate(captchaValue))
            throw new CaptchaException(); // Throw a custom exception if validation fails.

        //4. Expire captcha if _expireAfterValidation will be true
        if (_expireAfterValidation)
            capatchaServices.Expire();

        // 4. Call the next action filter or the action itself.
        await base.OnActionExecutionAsync(context, next);
    }

    /// <summary>
    /// Extracts the Captcha value from the request arguments.  This method supports various ways the Captcha
    /// value might be passed, including within a JSON object passed in the request body.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <returns>The Captcha value as a string, or null if not found.</returns>
    /// <exception cref="CaptchaException">Thrown if the Captcha value is not found in the request.</exception>
    private string FetchCaptchaValue(ActionExecutingContext context)
    {
        // 1. Iterate through the action arguments.
        var arguments = context.ActionArguments.ToArray();
        foreach (var argument in arguments)
        {
            // 2. Serialize the argument to JSON. This allows us to handle different input formats.
            var argumentSerialized = JsonConvert.SerializeObject(argument);

            // 3. Parse the serialized argument as a JsonDocument.
            using (var document = JsonDocument.Parse(argumentSerialized))
            {
                JsonElement jsonElement;
                JsonProperty elementObject;

                // 4. Try to get the "Value" property. This supports cases where the Captcha is nested.
                document.RootElement.TryGetProperty("Value", out jsonElement);

                // 5. Check if "CaptchaValue" exists directly under "Value".
                if (jsonElement.TryGetProperty("CaptchaValue", out JsonElement captchajsonElement))
                {
                    var capValue = captchajsonElement.GetString();
                    if (string.IsNullOrEmpty(capValue))
                        break;
                    else
                        return capValue;
                }

                // 6. If not directly under "Value", iterate through the properties of "Value" 
                //    to find "CaptchaValue" in nested objects.
                elementObject = jsonElement.EnumerateObject().First();

                while (jsonElement.TryGetProperty(elementObject.Name, out jsonElement))
                {
                    // 7. Check if the current property is "CaptchaValue".
                    if (elementObject.NameEquals("CaptchaValue"))
                    {
                        var capValue = jsonElement.GetString();
                        if (string.IsNullOrEmpty(capValue))
                            break;
                        else
                            return capValue;
                    }

                    elementObject = jsonElement.EnumerateObject().First(); // Move to the next nested object
                }
            }
        }

        // 8. Throw an exception if the Captcha value is not found.
        throw new CaptchaException("Captcha value is null");
    }
}

/// <summary>
/// Represents a request containing a Captcha value.  This class is typically used for deserialization
/// of the request body when the Captcha is submitted as part of a JSON payload.
/// </summary>
public class CaptchaValidationRequest
{
    /// <summary>
    /// The Captcha value.
    /// </summary>
    public string CaptchaValue { get; set; }
}