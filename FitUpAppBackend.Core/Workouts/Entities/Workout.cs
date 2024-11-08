using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.Workouts.Entities;

public sealed class Workout : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public DateTime Date { get; private set; }
    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    
    private List<WorkoutExercise> _workoutExercises = new();
    
    private Workout(Guid userId, DateTime date)
    {
        UserId = userId;
        Date = date.Date;
    }

    public static Workout Create(Guid userId, DateTime date)
     => new Workout(userId, date);

    public void AddExercise(WorkoutExercise exercise)
    {
        _workoutExercises.Add(exercise);
    }
}