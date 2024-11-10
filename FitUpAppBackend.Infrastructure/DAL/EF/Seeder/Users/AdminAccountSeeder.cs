using System.Security.Claims;
using FitUpAppBackend.Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder.AdminAccount;

public static class AdminAccountSeeder
{
    public static async Task SeedAsync(UserManager<User> userManager)
    {
        var admins = await userManager.GetUsersInRoleAsync(Core.Identity.Static.UserRoles.Admin);

        if (!admins.Any())
        {
            var admin = new User()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };
            
            var adminPassword = "Admin123$";
            
            await userManager.CreateAsync(admin, adminPassword);
            await userManager.AddToRoleAsync(admin, Core.Identity.Static.UserRoles.Admin);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, admin.Email), 
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
            };
            await userManager.AddClaimsAsync(admin, claims);
        }
    }
}