using FitUpAppBackend.Api.Filters;
using FluentValidation.AspNetCore;

namespace FitUpAppBackend.Api.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsPolicy(configuration);
        services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddFluentValidationAutoValidation();
        services.AddSwaggerConfig();

        return services;
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseSwaggerConfig();
        
        return app;
    }
}