using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Infrastructure.DAL.EF;

public static class Extensions
{
    public static IServiceCollection AddEFContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FitUpDB");
        
        services.AddDbContext<EFContext>(options =>
            options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        
        return services;
    }
}