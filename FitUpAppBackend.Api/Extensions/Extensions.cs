using System.Text;
using FitUpAppBackend.Core.Identity.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FitUpAppBackend.Api.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: Move auth section to infrastructure layer
        var authConfig = new AuthConfig();
        configuration.GetSection("AuthConfig").Bind(authConfig);
        
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = authConfig.JwtIssuer,
                ValidAudience = authConfig.JwtIssuer,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        
        services.AddAuthorization();
        services.AddCorsPolicy(configuration);
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddFluentValidationAutoValidation();
        services.AddSwaggerConfig();

        return services;
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseSwaggerConfig();
        
        return app;
    }
}