using FitUpAppBackend.Api.Filters;
using FluentValidation.AspNetCore;

namespace FitUpAppBackend.Api.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
        services.AddFluentValidationAutoValidation();

        return services;
    }
}