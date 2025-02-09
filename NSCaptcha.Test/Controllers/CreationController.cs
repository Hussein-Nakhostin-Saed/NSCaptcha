using Microsoft.AspNetCore.Mvc;
using NSCaptcha;

namespace NCaptcha.Test.Controllers;

[Route("api/captcha/create")]
[ApiController]
public class CreationController : ControllerBase
{
    private readonly ICaptchaService _captchaService;
    public CreationController(ICaptchaService captchaService)
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
