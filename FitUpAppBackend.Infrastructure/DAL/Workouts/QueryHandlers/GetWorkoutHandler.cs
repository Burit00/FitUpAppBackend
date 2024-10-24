using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Application.Workouts.Queries;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.Workouts.Exceptions;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Workouts.QueryHandlers;

public sealed class GetWorkoutHandler : IQueryHandler<GetWorkoutQuery, WorkoutDto>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetWorkoutHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<WorkoutDto> HandleAsync(GetWorkoutQuery query, CancellationToken cancellationToken)
    {
        var isWorkoutExist = await _context.Workouts.AnyAsync(w => w.UserId.Equals(_currentUserService.UserId) && w.Id.Equals(query.WorkoutId), cancellationToken);
        
        if (!isWorkoutExist)
            throw new WorkoutNotFoundException();
        
        var workout = await _context.Workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .ThenInclude(e => e.Category)
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.WorkoutSets)
            .ThenInclude(ws => ws.SetParameters)
            .ThenInclude(sp => sp.SetParameterName)
            .Where(w => _currentUserService.UserId.Equals(w.UserId))
            .FirstOrDefaultAsync(w => w.Id.Equals(query.WorkoutId), cancellationToken);
        
        return new WorkoutDto(workout);
    }
}