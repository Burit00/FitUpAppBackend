using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.QueryHandlers;

public class GetExerciseCategoriesHandler : IQueryHandler<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>>
{
    private readonly EFContext _context;

    public GetExerciseCategoriesHandler(EFContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ExerciseCategoryDto>> HandleAsync(GetExerciseCategoriesQuery query, CancellationToken cancellationToken)
    {
        var exerciseCategories = await _context.ExerciseCategories
            .Where(e => e.Name.ToLower().Contains(query.Search.ToLower()))
            .ToListAsync(cancellationToken);
        
        return exerciseCategories.Select(ec => new ExerciseCategoryDto(ec));
    }
}