using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.Workouts.Repositories;

namespace FitUpAppBackend.Infrastructure.DAL.Workouts.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    public Task<List<Workout>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Workout workout, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Workout workout, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}