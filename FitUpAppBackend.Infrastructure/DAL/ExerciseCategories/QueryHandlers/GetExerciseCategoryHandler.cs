using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategory;
using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.QueryHandlers;

public class GetExerciseCategoryHandler : IQueryHandler<GetExerciseCategoryQuery, ExerciseCategoryDto>
{
    private readonly IExerciseCategoryRepository _exerciseCategoryRepository;

    public GetExerciseCategoryHandler(IExerciseCategoryRepository exerciseCategoryRepository)
    {
        _exerciseCategoryRepository = exerciseCategoryRepository;
    }
    public async Task<ExerciseCategoryDto> HandleAsync(GetExerciseCategoryQuery query, CancellationToken cancellationToken)
    {
        var exerciseCategory = await _exerciseCategoryRepository.GetByIdAsync(query.ExerciseCategoryId, cancellationToken);
        return new ExerciseCategoryDto(exerciseCategory);
    }
}