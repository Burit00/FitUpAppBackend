using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.AdminAccount;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder;

public sealed class DatabaseInitializer(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService<EFContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (context is not null)
        {
            await context.Database.MigrateAsync(cancellationToken);
            
            await UserRolesSeeder.SeedAsync(roleManager, context, cancellationToken);
            await AdminAccountSeeder.SeedAsync(userManager, context, cancellationToken);
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}