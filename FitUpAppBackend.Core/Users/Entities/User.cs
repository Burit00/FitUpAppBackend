using FitUpAppBackend.Core.UserRoles.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.Users.Entities;

public class User : Entity
{
    public UserRole UserRole { get; set; }
    public string Login { get; private set; }
    public string Email { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string Password { get; private set; }
    
    public IReadOnlyCollection<Workout> Workouts => _workouts;
    
    private List<Workout> _workouts = new();
    
    private User(string login, string email, string password, DateOnly dateOfBirth)
    {
        Login = login;
        Email = email;
        Password = password;
        DateOfBirth = dateOfBirth;
    }
    
    public static User Create(string login, string email, string password, DateOnly dateOfBirth)
        => new(login, email, password, dateOfBirth);

    public void Update(string login, string email, string password, DateOnly dateOfBirth)
    {
        Login = login;
        Email = email;
        Password = password;
        DateOfBirth = dateOfBirth;
    }
}