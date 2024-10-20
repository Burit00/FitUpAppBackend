using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.Identity.Configuration;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Integrations.Email.Configurations;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Infrastructure.Common.Services;
using FitUpAppBackend.Infrastructure.DAL.EF;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Infrastructure.DAL.Identity.Services;
using FitUpAppBackend.Infrastructure.DAL.Workouts.Repositories;
using FitUpAppBackend.Infrastructure.Integrations.Email.Services;
using FitUpAppBackend.Shared;
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
        
        var emailConfig = new EmailConfig();
        configuration.GetSection("EmailConfig").Bind(emailConfig);
        services.AddSingleton(emailConfig);
        
        var authConfig = new AuthConfig();
        configuration.GetSection("AuthConfig").Bind(authConfig);
        services.AddSingleton(authConfig);

        services.AddHttpContextAccessor();
        
        services.AddScoped<IDateService, DateService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddScoped<IEmailService, EmailService>();
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        
        services.AddQueries();

        return services;
    }
}