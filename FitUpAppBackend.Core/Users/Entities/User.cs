using FitUpAppBackend.Core.UserRoles.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Core.Users.Entities;

public class User : IdentityUser<Guid>
{
    public UserRole UserRole { get; set; }
    public DateOnly DateOfBirth { get; private set; }
    
    public IReadOnlyCollection<Workout> Workouts => _workouts;
    
    private List<Workout> _workouts = new();
    
    private User(string email, string password, DateOnly dateOfBirth)
    {
        UserName = email;
        Email = email;
        PasswordHash = password;
        DateOfBirth = dateOfBirth;
    }
    
    public static User Create(string email, string password, DateOnly dateOfBirth)
        => new(email, password, dateOfBirth);

    public void Update(string email, string password, DateOnly dateOfBirth)
    {
        UserName = email;
        Email = email;
        PasswordHash = password;
        DateOfBirth = dateOfBirth;
    }
}