using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.WorkoutSets.Entities;

public sealed class WorkoutSet : Entity
{
    public uint OrderIndex { get; private set; }
    public Guid WorkoutExerciseId { get; private set; }
    public WorkoutExercise WorkoutExercise { get; private set; }
    public IReadOnlyCollection<SetParameter> SetParameters => _setParameters;
    
    private List<SetParameter> _setParameters = new();

    private WorkoutSet(uint orderIndex, WorkoutExercise workoutExercise)
    {
        OrderIndex = orderIndex;
        WorkoutExerciseId = workoutExercise.Id;
        WorkoutExercise = workoutExercise;
    }
    
    public static WorkoutSet Create(uint orderIndex, WorkoutExercise workoutExercise)
        => new WorkoutSet(orderIndex, workoutExercise);
}