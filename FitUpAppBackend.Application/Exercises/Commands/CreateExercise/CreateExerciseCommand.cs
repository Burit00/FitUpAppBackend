using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(string Name, Guid CategoryId, IEnumerable<Guid> SetParameterNameIds) : ICommand<CreateOrUpdateResponse>;