using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Application.Exercises.Queries.GetExercise;
using FitUpAppBackend.Core.Exercises.Exceptions;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Exercises.QueryHandlers;

public sealed class GetExerciseQueryHandler : IQueryHandler<GetExerciseQuery, ExerciseDetailsDto>
{
    private readonly EFContext _context;

    public GetExerciseQueryHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDetailsDto> HandleAsync(GetExerciseQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Exercises
            .Include(e => e.SetParameters)
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id.Equals(query.Id), cancellationToken);

        if (result is null)
            throw new ExerciseNotFoundException(query.Id);

        return new ExerciseDetailsDto(result);
    }
}