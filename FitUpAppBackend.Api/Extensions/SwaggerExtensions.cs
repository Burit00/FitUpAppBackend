using Microsoft.OpenApi.Models;

namespace FitUpAppBackend.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter \"Bearer \" and then paste your valid token."
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "FitUp API",
                Version = "v1",
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "FitUP API V1");
            opt.EnablePersistAuthorization();
        });

        return app;
    }
}