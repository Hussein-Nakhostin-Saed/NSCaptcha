using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NSCaptcha;
using NSCaptcha.Test.Services;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDataProtection().SetApplicationName("CaptchaValidator").PersistKeysToFileSystem(new DirectoryInfo(Path.Combine("C:/Users/AL1989/Desktop")));
builder.Services.AddMemoryCache();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddCaptcha(options =>
{
    options.Content.UseLetters(uselowerCase: true, useUpperCase: false);
    options.Style.AddNoiseLinesColors(Color.Blue, Color.Brown, Color.BurlyWood);
    //.
    //.
    //.
    // and other options
});

builder.Services.AddCaptcha((options, cookieOptions) =>
{
    options.Content.UseLetters(uselowerCase: true, useUpperCase: false);
    options.Style.AddNoiseLinesColors(Color.Blue, Color.Brown, Color.BurlyWood);
    cookieOptions.HttpOnly = true;
    cookieOptions.Name = "captcha-cookie-name";
    //.
    //.
    //.
    // and other options
});

builder.Services.AddCaptcha();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ValidateCaptchaTestAttribute>();
//});

var app = builder.Build();

//app.Use(async (context, next) =>
//{
//    context.Request.EnableBuffering();
//    await next();
//});
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
