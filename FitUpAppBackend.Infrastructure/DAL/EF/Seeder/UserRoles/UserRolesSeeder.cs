using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder.UserRoles;

public static class UserRolesSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager, EFContext context, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var roles = Core.Identity.Static.UserRoles.Roles;
        foreach (var role in roles)
        {
            var isRoleExist = await roleManager.RoleExistsAsync(role.Name);

            if (!isRoleExist)
                await roleManager.CreateAsync(new IdentityRole<Guid>(role.Name));
        }

        await transaction.CommitAsync(cancellationToken);
    }
}