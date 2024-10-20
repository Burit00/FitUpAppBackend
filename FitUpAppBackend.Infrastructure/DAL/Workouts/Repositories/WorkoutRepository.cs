using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.Workouts.Exceptions;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Workouts.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Workout> _workouts;

    public WorkoutRepository(EFContext context)
    {
        _context = context;
        _workouts = _context.Workouts;
    }

    public async Task<List<Workout>> GetAllAsync(CancellationToken cancellationToken)
        => await _workouts.ToListAsync(cancellationToken);

    public Task<List<Workout>> GetAllForUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var workouts = _workouts.Where(w => w.UserId.Equals(userId));
        return workouts.ToListAsync(cancellationToken);
    }


    public async Task<Workout> GetAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        var workout = await _workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .ThenInclude(e => e.Category)
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.WorkoutSets)
            .ThenInclude(ws => ws.SetParameters)
            .ThenInclude(sp => sp.SetParameterName)
            .FirstOrDefaultAsync(w => w.Id.Equals(workoutId), cancellationToken);

        if (workout is null)
            throw new WorkoutNotFoundException();
        
        return workout;
    }

    public async Task<Guid> CreateAsync(Workout workout, CancellationToken cancellationToken)
    {
        var result = await _workouts.AddAsync(workout, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public Task DeleteAsync(Workout workout, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}