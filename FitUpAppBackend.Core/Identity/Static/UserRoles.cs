using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Core.Identity.Static;

public static class UserRoles
{
    public const string Admin = nameof(Admin); 
    public const string User = nameof(User);

    private static List<IdentityRole> _roles;

    static UserRoles()
    {
        _roles = new()
        {
            new IdentityRole(Admin),
            new IdentityRole(User),
        };
    }
    
    public static IReadOnlyList<IdentityRole> Roles => _roles;
}