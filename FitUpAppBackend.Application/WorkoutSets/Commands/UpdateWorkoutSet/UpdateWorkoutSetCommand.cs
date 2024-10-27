using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.WorkoutSets.Commands.Shared;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.UpdateWorkoutSet;

public sealed record UpdateWorkoutSetCommand(Guid Id, int OrderIndex, IEnumerable<WorkoutSetParameterValue> ParameterValues) : ICommand<CreateOrUpdateResponse>
{

}

