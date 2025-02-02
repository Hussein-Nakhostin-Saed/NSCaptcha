using Microsoft.AspNetCore.Mvc.Filters;
using NCaptcha;

namespace NSCaptcha.Test.Middlewares;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exp = context.Exception;
        if (exp is InvalidCaptchaException bizException)
        {
            Console.WriteLine(bizException.Message);
        }
    }
}
