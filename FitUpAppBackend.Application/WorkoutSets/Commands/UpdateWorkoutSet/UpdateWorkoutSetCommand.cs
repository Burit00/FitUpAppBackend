using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.UpdateWorkoutSet;

public sealed record UpdateWorkoutSetCommand(Guid Id, int OrderIndex) : ICommand<CreateOrUpdateResponse>
{

}

