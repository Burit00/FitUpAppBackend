using FitUpAppBackend.Core.Identity;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Infrastructure.Common.Swagger;
using FitUpAppBackend.Infrastructure.DAL.EF;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Infrastructure.DAL.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.Password.RequireNonAlphanumeric = true;
            o.Password.RequiredLength = 8;
            o.Password.RequiredUniqueChars = 4;
            
            o.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<EFContext>()
            .AddDefaultTokenProviders();
        
        services.AddEFContext(configuration);
        
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSwaggerConfig();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}