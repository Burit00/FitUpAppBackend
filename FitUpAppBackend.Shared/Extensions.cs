using System.Reflection;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using FitUpAppBackend.Shared.Commands;
using FitUpAppBackend.Shared.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Shared;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        
        var assembly = Assembly.GetCallingAssembly();

        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        
        return services;
    }
    
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        
        var assembly = Assembly.GetCallingAssembly();

        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        
        return services;
    }
}