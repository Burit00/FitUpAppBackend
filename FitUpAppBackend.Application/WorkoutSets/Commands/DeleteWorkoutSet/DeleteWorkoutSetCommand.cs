using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.DeleteWorkoutSet;

public sealed record DeleteWorkoutSetCommand(Guid Id) : ICommand
{

}

