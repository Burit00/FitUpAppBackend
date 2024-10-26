using FitUpAppBackend.Application.WorkoutExercises.DTO;
using FitUpAppBackend.Application.WorkoutExercises.Queries.GetWorkoutExercise;
using FitUpAppBackend.Core.WorkoutExercises.Exceptions;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.WorkoutExercises.QueryHandlers;

public sealed class GetWorkoutExerciseHandler: IQueryHandler<GetWorkoutExerciseQuery, WorkoutExerciseDetailsDto>
{
    private readonly EFContext _context;

    public GetWorkoutExerciseHandler(EFContext context)
    {
        _context = context;
    }
    public async Task<WorkoutExerciseDetailsDto> HandleAsync(GetWorkoutExerciseQuery query, CancellationToken cancellationToken)
    {
        var workoutExercise = await _context.WorkoutExercises
            .Include(we => we.Exercise)
            .ThenInclude(e => e.SetParameters)
            .Include(we => we.WorkoutSets)
            .ThenInclude(we => we.SetParameters)
            .FirstOrDefaultAsync(we => we.Id == query.Id, cancellationToken);

        if (workoutExercise is null)
            throw new WorkoutExerciseNotFoundException();

        return new WorkoutExerciseDetailsDto(workoutExercise);
    }
}