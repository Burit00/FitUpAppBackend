using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.DeleteWorkoutExercise;

public sealed record DeleteWorkoutExerciseCommand(Guid WorkoutExerciseId) : ICommand;