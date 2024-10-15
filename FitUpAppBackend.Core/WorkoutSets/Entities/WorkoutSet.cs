using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.WorkoutSets.Entities;

public sealed class WorkoutSet : Entity
{
    public uint OrderIndex { get; private set; }
    public Guid WorkoutExerciseId { get; private set; }
    public WorkoutExercise WorkoutExercise { get; private set; }
    public IReadOnlyCollection<SetParameter> SetParameters => _setParameters;
    
    private List<SetParameter> _setParameters = new();

    private WorkoutSet(uint orderIndex, Guid workoutExerciseId)
    {
        OrderIndex = orderIndex;
        WorkoutExerciseId = workoutExerciseId;
    }
    
    public static WorkoutSet Create(uint orderIndex, Guid workoutExerciseId)
        => new WorkoutSet(orderIndex, workoutExerciseId);
}