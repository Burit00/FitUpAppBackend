using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Core.Workouts.Repositories;

public interface IWorkoutRepository
{
    public Task<List<Workout>> GetAllAsync(CancellationToken cancellationToken);
    public Task<List<Workout>> GetAllForUserAsync(Guid userId, CancellationToken cancellationToken);
    public Task<Guid> CreateAsync(Workout workout, CancellationToken cancellationToken);
    public Task DeleteAsync(Workout workout, CancellationToken cancellationToken);
}