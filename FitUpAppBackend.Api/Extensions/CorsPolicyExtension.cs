using FitUpAppBackend.Shared.Configurations.Cors;

namespace FitUpAppBackend.Api.Extensions;

public static class CorsPolicyExtension
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsConfig = new CorsConfig();
        configuration.GetSection("Cors").Bind(corsConfig);

        services.AddCors(cors =>
        {
            var allowedHeaders = corsConfig.AllowedHeaders;
            var allowedMethods = corsConfig.AllowedMethods;
            var allowedOrigins = corsConfig.AllowedOrigins;
            var exposedHeaders = corsConfig.ExposedHeaders;
            cors.AddPolicy("CorsPolicy", corsBuilder =>
            {
                if (corsConfig.AllowCredentials)
                    corsBuilder.AllowCredentials();
                else
                    corsBuilder.DisallowCredentials();


                corsBuilder
                    .WithOrigins(["http://localhost:3000"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}