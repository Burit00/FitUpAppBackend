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

    public Task<Workout> GetAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(Workout workout, CancellationToken cancellationToken)
    {
        var isWorkoutExist = _workouts.Any(w => w.UserId.Equals(workout.UserId) && w.Date.Equals(workout.Date));
    
        if (isWorkoutExist)
            throw new WorkoutForUserAlreadyExistException();
        
        var result = await _workouts.AddAsync(workout, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public Task UpdateAsync(Workout workout, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}