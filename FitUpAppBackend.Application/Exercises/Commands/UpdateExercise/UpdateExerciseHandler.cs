using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.Exercises.Exceptions;
using FitUpAppBackend.Core.Exercises.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseHandler : ICommandHandler<UpdateExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(command.Id, cancellationToken);
        if(exercise is null)
            throw new ExerciseNotFoundException(command.Id);
        
        exercise.Update(command.Name, command.CategoryId);
        var result = await _exerciseRepository.UpdateAsync(exercise, cancellationToken);
        return new CreateOrUpdateResponse(result);
    }
}