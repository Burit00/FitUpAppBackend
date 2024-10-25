using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;
using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.QueryHandlers;

public class GetExerciseCategoriesHandler : IQueryHandler<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>>
{
    private readonly IExerciseCategoryRepository _exerciseCategoryRepository;

    public GetExerciseCategoriesHandler(IExerciseCategoryRepository exerciseCategoryRepository)
    {
        _exerciseCategoryRepository = exerciseCategoryRepository;
    }
    public async Task<IEnumerable<ExerciseCategoryDto>> HandleAsync(GetExerciseCategoriesQuery query, CancellationToken cancellationToken)
    {
        var exerciseCategories = await _exerciseCategoryRepository.GetAllAsync(query.Search, cancellationToken);
        return exerciseCategories.Select(c => new ExerciseCategoryDto(c));
    }
}