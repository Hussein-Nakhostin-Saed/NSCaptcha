using Microsoft.AspNetCore.Mvc;
using NSCaptcha;
using System.Text;

namespace NCaptcha.Test.Controllers;

[Route("api/captcha")]
[ApiController]
public class ValidationController : ControllerBase
{
    private readonly ICaptchaService _captchaService;
    public ValidationController(ICaptchaService captchaService)
    {
        _captchaService = captchaService;
    }

    [Route("validate")]
    [HttpPost]
    [ValidateCaptcha]
    public async Task Validate(CaptchaTest6 valued)
    {
    }
}

public class CaptchaTest
{
    public CaptchaTest2 CaptchaTest2 { get; set; }
}

public class CaptchaTest2
{
    public CaptchaTest3 CaptchaTest3 { get; set; }
}
public class CaptchaTest3
{
    public CaptchaTest4 CaptchaTest4 { get; set; }
}

public class CaptchaTest4
{
    public CaptchaTest5 CaptchaTest5 { get; set; }
}

public class CaptchaTest5
{
    public CaptchaTest6 CaptchaTest6 { get; set; }
}

public class CaptchaTest6
{
    public string? CaptchaValue { get; set; }
}
