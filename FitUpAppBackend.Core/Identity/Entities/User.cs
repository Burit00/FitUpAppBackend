using FitUpAppBackend.Core.Workouts.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Core.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public DateTimeOffset? DateOfBirth { get; private set; }

    public IReadOnlyCollection<Workout> Workouts => _workouts;

    private List<Workout> _workouts = new();
}