using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FitUpAppBackend.Infrastructure.Common.Swagger;

public static class Extensions
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "FitUp API",
                Version = "v1",
            });
        });
        
        return services;
    }
}