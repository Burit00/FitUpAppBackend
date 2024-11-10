using System.Security.Claims;
using FitUpAppBackend.Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder.Users;

public static class TestUserAccountSeeder
{
    private static UserRecord _testUser = new UserRecord(
        "test@test.pl",
        "test@test.pl",
        "Test123$"
    );

    public static async Task SeedAsync(UserManager<User> userManager)
    {
        var testUser = await userManager.FindByEmailAsync(_testUser.Email);
        if (testUser != null) return;

        testUser = new User()
        {
            Email = _testUser.Email,
            UserName = _testUser.UserName,
        };
        
        await userManager.CreateAsync(testUser, _testUser.Password);
        await userManager.AddToRoleAsync(testUser, Core.Identity.Static.UserRoles.User);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, testUser.Email), 
            new Claim(ClaimTypes.NameIdentifier, testUser.Id.ToString())
        };
        await userManager.AddClaimsAsync(testUser, claims);
    }

    private record UserRecord(string Email, string UserName, string Password);
}