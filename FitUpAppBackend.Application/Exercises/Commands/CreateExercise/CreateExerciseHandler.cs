using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.Exercises.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseHandler : ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateExerciseCommand command,
        CancellationToken cancellationToken)
    {
        var exercise = Exercise.Create(command.Name, command.CategoryId);
        var result = await _exerciseRepository.CreateAsync(exercise, command.SetParameterNameIds, cancellationToken);
        
        return new CreateOrUpdateResponse(result);
    }
}