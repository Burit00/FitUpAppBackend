using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Core.WorkoutExercises.Entities;

public class WorkoutExercise : Entity
{
    public int OrderIndex { get; private set; }
    public Guid ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public Guid WorkoutId { get; private set; }
    public Workout Workout { get; private set; }
    public IReadOnlyList<WorkoutSet> WorkoutSets => _workoutSets;
    
    private List<WorkoutSet> _workoutSets = new();

    private WorkoutExercise(int orderIndex, Guid exerciseId, Guid workoutId)
    {
        OrderIndex = orderIndex;
        ExerciseId = exerciseId;
        WorkoutId = workoutId;
    }
    
    public static WorkoutExercise Create(int orderIndex, Guid exerciseId, Guid workoutId)
        => new WorkoutExercise(orderIndex, exerciseId, workoutId);

    public void Update(int orderIndex)
    {
        OrderIndex = orderIndex;
    }
    
    public void AddRangeWorkoutSet(IEnumerable<WorkoutSet> workoutSets)
    {
        _workoutSets.AddRange(workoutSets);
    }
}