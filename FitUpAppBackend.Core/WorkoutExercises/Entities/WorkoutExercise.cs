using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Core.WorkoutExercises.Entities;

public class WorkoutExercise : Entity
{
    public ushort OrderIndex { get; private set; }
    public Guid ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public IReadOnlyList<WorkoutSet> WorkoutSets => _workoutSets;
    
    private List<WorkoutSet> _workoutSets = new();

    private WorkoutExercise(ushort orderIndex, Guid exerciseId)
    {
        OrderIndex = orderIndex;
        ExerciseId = exerciseId;
    }
    
    public static WorkoutExercise Create(ushort orderIndex, Guid exerciseId)
        => new WorkoutExercise(orderIndex, exerciseId);

    public void AddRangeWorkoutSet(IEnumerable<WorkoutSet> workoutSets)
    {
        _workoutSets.AddRange(workoutSets);
    }
}