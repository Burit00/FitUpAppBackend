using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Core.WorkoutSets.Repositories;

public interface IWorkoutSetRepository
{
    public Task<Guid> CreateAsync(WorkoutSet workoutSet, CancellationToken cancellationToken);  
    public Task<Guid> UpdateAsync(WorkoutSet workoutSet, CancellationToken cancellationToken);
    public Task<WorkoutSet> GetByIdAsync(Guid workoutSetId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}