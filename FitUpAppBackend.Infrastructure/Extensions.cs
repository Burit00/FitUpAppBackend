﻿using FitUpAppBackend.Core.Identity;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Infrastructure.DAL.EF;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Infrastructure.DAL.Identity.Services;
using FitUpAppBackend.Infrastructure.Integrations.Email.Configuration;
using FitUpAppBackend.Infrastructure.Integrations.Email.Services;
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

        services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}