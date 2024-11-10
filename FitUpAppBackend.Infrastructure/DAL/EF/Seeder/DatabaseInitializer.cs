using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.AdminAccount;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.CategoriesAndExercises;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.SetParameterNames;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.UserRoles;
using FitUpAppBackend.Infrastructure.DAL.EF.Seeder.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder;

public sealed class DatabaseInitializer(IServiceProvider serviceProvider, IWebHostEnvironment environment) : IHostedService
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
            
            await AdminAccountSeeder.SeedAsync(userManager);
            if (environment.IsDevelopment())
            {
                await TestUserAccountSeeder.SeedAsync(userManager);
            }
            
            await SetParameterNamesSeeder.SeedAsync(context, cancellationToken);
            await CategoriesAndExercisesSeeder.CategoriesSeedAsync(context, cancellationToken);
            await CategoriesAndExercisesSeeder.ExerciseSeedAsync(context, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}