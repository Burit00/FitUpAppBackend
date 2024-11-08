using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed record CreateWorkoutCommand(
        DateTime Date,
        IEnumerable<Guid> ExerciseIds
    ) : ICommand<CreateOrUpdateResponse>;