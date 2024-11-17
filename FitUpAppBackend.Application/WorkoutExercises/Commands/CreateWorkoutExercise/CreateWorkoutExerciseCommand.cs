using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.CreateWorkoutExercise;

public sealed record CreateWorkoutExerciseCommand(Guid ExerciseId, Guid WorkoutId, int OrderIndex = -1)
    : ICommand<CreateOrUpdateResponse>;