using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Exercises.Commands.UpdateExercise;

public sealed record UpdateExerciseCommand(Guid Id, string? Name, Guid? CategoryId) : ICommand<CreateOrUpdateResponse>;