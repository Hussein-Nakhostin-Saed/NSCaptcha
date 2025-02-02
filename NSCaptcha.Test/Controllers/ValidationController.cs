using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NCaptcha.Test.Controllers;

[Route("api/captcha")]
[ApiController]
public class ValidationController : ControllerBase
{
    private readonly ICaptchaServices _captchaService;
    public ValidationController(ICaptchaServices captchaService)
    {
        _captchaService = captchaService;
    }

    [Route("validate")]
    [HttpPost]
    [ValidateCaptcha]
    public async Task Validate([FromBody] CaptchaTest value)
    {
        
    }
}

public class CaptchaTest
{
    public string CaptchaValue { get; set; }
}