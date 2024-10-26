using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Application.Exercises.Queries.GetExercises;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Exercises.QueryHandlers;

public sealed class GetExercisesQueryHandler : IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    private readonly EFContext _context;

    public GetExercisesQueryHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExerciseDto>> HandleAsync(GetExercisesQuery query,
        CancellationToken cancellationToken)
    {
        var result = _context.Exercises
            .Include(e => e.Category)
            .Where(e => e.Name.ToLower().Contains(query.Search.ToLower()));
        
        if (query.ExerciseCategoryIds.Any())
            result = result.Where(e => query.ExerciseCategoryIds.Contains(e.CategoryId));
        
        return (await result.ToListAsync(cancellationToken)).Select(exercise => new ExerciseDto(exercise));
    }
}