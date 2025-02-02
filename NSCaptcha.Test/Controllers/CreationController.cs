using Microsoft.AspNetCore.Mvc;

namespace NCaptcha.Test.Controllers;

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
