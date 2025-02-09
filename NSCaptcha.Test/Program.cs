using Microsoft.AspNetCore.DataProtection;
using NSCaptcha;
using NSCaptcha.Test.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDataProtection().SetApplicationName("CaptchaValidator").PersistKeysToFileSystem(new DirectoryInfo(Path.Combine("C:/Users/Hussein Nakhostin/Desktop")));
builder.Services.AddMemoryCache();

builder.Services.AddCaptcha();



var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
