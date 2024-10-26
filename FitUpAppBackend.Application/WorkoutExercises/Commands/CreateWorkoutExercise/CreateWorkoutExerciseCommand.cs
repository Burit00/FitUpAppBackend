using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.CreateWorkoutExercise;

public sealed record CreateWorkoutExerciseCommand(ushort OrderIndex, Guid ExerciseId, Guid WorkoutId)
    : ICommand<CreateOrUpdateResponse>;