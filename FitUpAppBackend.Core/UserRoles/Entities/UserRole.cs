using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.UserRoles.Entities;

public class UserRole : Entity
{
    public UserRoleEnum Role { get; set; }
}