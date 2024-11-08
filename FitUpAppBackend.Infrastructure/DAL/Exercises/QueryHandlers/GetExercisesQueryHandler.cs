using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Application.Exercises.Queries.GetExercises;
using FitUpAppBackend.Core.Exercises.Entities;
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

        if (query.ExerciseCategoryIds != null && query.ExerciseCategoryIds.Any())
            result = result.Where(e => query.ExerciseCategoryIds.Contains(e.CategoryId));

        var exercises = await result.ToListAsync(cancellationToken);

        return exercises.Select(exercise => new ExerciseDto(exercise));
    }

    private bool ContainsSearch(Exercise exercise, string search) =>
        exercise.Name.ToLower().Contains(search.ToLower());
}