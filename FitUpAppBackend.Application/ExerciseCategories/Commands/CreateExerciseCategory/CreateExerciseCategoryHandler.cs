using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.ExerciseCategories.Commands.CreateExerciseCategory;

public class CreateExerciseCategoryHandler : ICommandHandler<CreateExerciseCategoryCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseCategoryRepository _exerciseCategoryRepository;

    public CreateExerciseCategoryHandler(IExerciseCategoryRepository exerciseCategoryRepository)
    {
        _exerciseCategoryRepository = exerciseCategoryRepository;
    }
    
    public async Task<CreateOrUpdateResponse> HandleAsync(CreateExerciseCategoryCommand command, CancellationToken cancellationToken)
    {
        var exerciseCategory = ExerciseCategory.Create(command.Name);
        var result = await _exerciseCategoryRepository.CreateAsync(exerciseCategory, cancellationToken);

        return new CreateOrUpdateResponse(result);
    }
}