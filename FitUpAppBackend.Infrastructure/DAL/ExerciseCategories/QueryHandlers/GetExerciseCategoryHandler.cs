using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategory;
using FitUpAppBackend.Core.ExerciseCategories.Exceptions;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.QueryHandlers;

public class GetExerciseCategoryHandler : IQueryHandler<GetExerciseCategoryQuery, ExerciseCategoryDto>
{
    private readonly EFContext _context;

    public GetExerciseCategoryHandler(EFContext context)
    {
        _context = context;
    }
    public async Task<ExerciseCategoryDto> HandleAsync(GetExerciseCategoryQuery query, CancellationToken cancellationToken)
    {
        var exerciseCategory = await _context.ExerciseCategories.FirstOrDefaultAsync(ec => ec.Id.Equals(query.ExerciseCategoryId), cancellationToken);
        
        if(exerciseCategory is null)
            throw new ExerciseCategoryNotFoundException(query.ExerciseCategoryId);
        
        return new ExerciseCategoryDto(exerciseCategory);
    }
}