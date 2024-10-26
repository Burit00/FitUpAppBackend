using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.WorkoutExercises.Repositories;

public interface IWorkoutExerciseRepository
{
    public Task<IEnumerable<WorkoutExercise>> GetAllAsync(CancellationToken cancellationToken);
    public Task<WorkoutExercise> GetAsync(Guid workoutId, CancellationToken cancellationToken);
    public Task<Guid> CreateAsync(WorkoutExercise workout, CancellationToken cancellationToken);
    public Task<Guid> UpdateAsync(WorkoutExercise workout, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid workoutId, CancellationToken cancellationToken);
}