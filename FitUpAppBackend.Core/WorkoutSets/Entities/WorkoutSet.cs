using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.WorkoutSets.Entities;

public sealed class WorkoutSet : Entity
{
    public int OrderIndex { get; private set; }
    public Guid WorkoutExerciseId { get; private set; }
    public WorkoutExercise WorkoutExercise { get; private set; }
    public IReadOnlyCollection<SetParameter> SetParameters => _setParameters;

    private List<SetParameter> _setParameters = new();

    private WorkoutSet(int orderIndex, Guid workoutExerciseId)
    {
        OrderIndex = orderIndex;
        WorkoutExerciseId = workoutExerciseId;
    }

    public static WorkoutSet Create(int orderIndex, Guid workoutExerciseId)
        => new WorkoutSet(orderIndex, workoutExerciseId);

    public void Update(int orderIndex)
    {
        OrderIndex = orderIndex;
    }

    public void AddSetParameterRange(IEnumerable<SetParameter> setParameters)
    {
        _setParameters.AddRange(setParameters);
    }
}