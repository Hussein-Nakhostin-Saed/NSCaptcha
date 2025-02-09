using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace NSCaptcha;

/// <summary>
/// This class implements an action filter that validates a Captcha challenge before 
/// allowing the execution of a controller action.
/// </summary>
public class ValidateCaptchaAttribute : ActionFilterAttribute
{
    /// <summary>
    /// This method is called asynchronously before the action method is executed.
    /// It retrieves the Captcha services from the dependency injection container, 
    /// reads the Captcha value from the request body, validates it, and throws an exception
    /// if the validation fails. If the Captcha is valid, the action method is allowed to proceed.
    /// </summary>
    /// <param name="context">The action execution context containing information about the current request and action.</param>
    /// <param name="next">The delegate to invoke to execute the action method.</param>
    /// <returns>An asynchronous task representing the completion of the action filter execution.</returns>
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get the ICaptchaServices instance used for Captcha validation.
        var capatchaServices = context.HttpContext.RequestServices.GetService<ICaptchaService>()!;

        // Get the HttpRequest object representing the incoming request.
        var request = context.HttpContext.Request;

        // Access the request body stream containing the request data.
        var body = request.Body;

        // Reset the stream position to the beginning to ensure we can read the entire body.
        body.Seek(0, SeekOrigin.Begin);

        // Create a StreamReader to read the request body content as a string.
        using (var reader = new StreamReader(body))
        {
            // Read the entire request body as a string asynchronously.
            var requestBody = await reader.ReadToEndAsync();

            // Deserialize the request body string into a CaptchaValidationRequest object containing the Captcha value.
            var captchaValidationRequest = JsonConvert.DeserializeObject<CaptchaValidationRequest>(requestBody);

            // Validate the Captcha value using the injected ICaptchaServices service.
            if (!capatchaServices.Validate(captchaValidationRequest.CaptchaValue))
                throw new CaptchaException();
        }

        // If the Captcha validation passes, call the next delegate in the filter pipeline to execute the action method.
        await base.OnActionExecutionAsync(context, next);
    }
}

/// <summary>
/// This class defines a simple data structure to hold the Captcha value submitted by the client.
/// </summary>
public class CaptchaValidationRequest
{
    /// <summary>
    /// The Captcha value submitted by the client.
    /// </summary>
    public string CaptchaValue { get; set; }
}