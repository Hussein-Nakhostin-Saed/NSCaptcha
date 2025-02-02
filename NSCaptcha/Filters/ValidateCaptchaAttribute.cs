using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace NCaptcha;

public class ValidateCaptchaAttribute : ActionFilterAttribute
{
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var capatchaServices = context.HttpContext.RequestServices.GetService<ICaptchaServices>()!;
        var request = context.HttpContext.Request;

        var body = request.Body;
        body.Seek(0, SeekOrigin.Begin);

        using (var reader = new StreamReader(body))
        {
            var requestBody = await reader.ReadToEndAsync();
            var captchaValidationRequest = JsonConvert.DeserializeObject<CaptchaValidationRequest>(requestBody);

            if (!capatchaServices.Validate(captchaValidationRequest.CaptchaValue))
                throw new InvalidCaptchaException();
        }

        await base.OnActionExecutionAsync(context, next);
    }
}

public class CaptchaValidationRequest
{
    public string CaptchaValue { get; set; }
}