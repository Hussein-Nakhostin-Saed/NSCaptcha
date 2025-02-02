using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NCaptcha;
using NSCaptcha.Test.Middlewares;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var captchaBuilder = CaptchaServiceCollection.CreateBuilder(builder.Services);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

AddControllers();
builder.Services.AddDataProtection().SetApplicationName("CaptchaValidator").PersistKeysToFileSystem(new DirectoryInfo(Path.Combine("C:/Users/AL1989/Desktop")));
builder.Services.AddMemoryCache();

captchaBuilder.AddCaptcha()
                .AddCaptchaTokenEncryptionService<DefaultDataProtectionService>()
                .AddCaptchaTokenSerializer<InCookieCaptchaTokenSerializer>()
                .AddCaptchaTokenCacheService<InMemoryCaptchaTokenCacheService>()
                .Build();




void AddControllers()
{
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();

    }).AddJsonOptions(options =>
    {
        var enumConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(enumConverter);
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }).ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var modelState = actionContext.ModelState.Values;

            StringBuilder sb = new StringBuilder();
            if (!actionContext.ModelState.IsValid)
            {
                foreach (var item in actionContext.ModelState)
                {
                    foreach (var err in item.Value.Errors)
                    {
                        sb.AppendLine(err.ErrorMessage);
                    }
                }
            }

            object response;
            response = new
            {
                code = -1,
                message = sb.ToString()
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        };
    });
}




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
