using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed record CreateWorkoutCommand(
        DateTimeOffset Date
    ) : ICommand<Guid>;
        DateTime Date