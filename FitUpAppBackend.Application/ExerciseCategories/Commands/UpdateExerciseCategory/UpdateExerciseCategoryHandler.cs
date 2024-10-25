using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.ExerciseCategories.Commands.UpdateExerciseCategory;

public class UpdateExerciseCategoryHandler : ICommandHandler<UpdateExerciseCategoryCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseCategoryRepository _exerciseCategoryRepository;

    public UpdateExerciseCategoryHandler(IExerciseCategoryRepository exerciseCategoryRepository)
    {
        _exerciseCategoryRepository = exerciseCategoryRepository;
    }
    
    public async Task<CreateOrUpdateResponse> HandleAsync(UpdateExerciseCategoryCommand command, CancellationToken cancellationToken)
    {
        
        var exerciseCategory = await _exerciseCategoryRepository.GetByIdAsync(command.Id, cancellationToken);
        exerciseCategory.Update(command.Name);
        
        var result = await _exerciseCategoryRepository.UpdateAsync(exerciseCategory, cancellationToken);

        return new CreateOrUpdateResponse(result);
    }
}