using Swashbuckle.AspNetCore.SwaggerUI;
using TajneedApi.Api.Middleware;

namespace TajneedApi.Api.Extensions;

public static class ConfigurationExtensions
{
    public static void ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }

    public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "IDH API v2");
            options.OAuthClientId(configuration.GetValue<string>("Jwt:Audience"));
            options.OAuthUsePkce();
            options.EnablePersistAuthorization();
            options.DefaultModelsExpandDepth(1);
            options.DocExpansion(DocExpansion.None);
            options.EnableTryItOutByDefault();
        });
    }
}