using FitUpAppBackend.Core.Users.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.Workouts.Entities;

public sealed class Workout : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    
    private List<WorkoutExercise> _workoutExercises => new();
    
    private Workout(User user, DateTimeOffset date)
    {
        User = user;
        UserId = user.Id;
        Date = date;
    }

    public static Workout Create(User user, DateTimeOffset date)
     => new Workout(user, date);

    public void AddExercise(WorkoutExercise exercise)
    {
        _workoutExercises.Add(exercise);
    }
}