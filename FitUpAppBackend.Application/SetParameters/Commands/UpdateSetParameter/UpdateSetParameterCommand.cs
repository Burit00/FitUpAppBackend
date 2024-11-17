using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.SetParameters.Commands.UpdateSetParameter;

public sealed record UpdateSetParameterRangeCommand(IEnumerable<UpdateSetParameterCommand> Parameters, Guid WorkoutSetId) : ICommand;

public sealed record UpdateSetParameterCommand(Guid Id, string Value) : ICommand;