﻿using System.Reflection;
using FitUpAppBackend.Shared;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}