using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.WorkoutSets.Commands.Shared;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.CreateWorkoutSet;

public sealed record CreateWorkoutSetCommand(Guid WorkoutExerciseId, int OrderIndex, IEnumerable<WorkoutSetParameterValue> ParameterValues) : ICommand<CreateOrUpdateResponse>
{

}

