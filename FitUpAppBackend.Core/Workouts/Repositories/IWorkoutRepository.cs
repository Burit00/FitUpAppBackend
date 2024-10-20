using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Core.Workouts.Repositories;

public interface IWorkoutRepository
{
    public Task<List<Workout>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Workout> GetAsync(Guid workoutId, CancellationToken cancellationToken);
    public Task<Guid> CreateAsync(Workout workout, CancellationToken cancellationToken);
    public Task UpdateAsync(Workout workout, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid workoutId, CancellationToken cancellationToken);
}