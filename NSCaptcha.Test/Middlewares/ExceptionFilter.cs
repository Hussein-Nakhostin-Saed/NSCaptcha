using Microsoft.AspNetCore.Mvc.Filters;
using NSCaptcha;

namespace NSCaptcha.Test.Middlewares;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exp = context.Exception;
        if (exp is CaptchaException bizException)
        {
            Console.WriteLine(bizException.Message);
        }
    }
}
