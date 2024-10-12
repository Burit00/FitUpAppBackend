using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.WorkoutExercises.Entities;

public class WorkoutExercise : Entity
{
    public ushort OrderIndex { get; private set; }
    public Guid ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public IReadOnlyList<WorkoutSet> WorkoutSets => _workoutSets;
    
    private List<WorkoutSet> _workoutSets = new();

    private WorkoutExercise(ushort orderIndex, Guid exerciseId, Exercise exercise)
    {
        OrderIndex = orderIndex;
        ExerciseId = exerciseId;
        Exercise = exercise;
    }
    
    public static WorkoutExercise Create(ushort orderIndex, Guid exerciseId, Exercise exercise)
        => new WorkoutExercise(orderIndex, exerciseId, exercise);

    public void AddWorkoutSet(WorkoutSet workoutSet)
    {
        _workoutSets.Add(workoutSet);
    }
}