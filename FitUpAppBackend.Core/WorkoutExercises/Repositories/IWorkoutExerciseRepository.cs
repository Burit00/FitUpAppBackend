using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.WorkoutExercises.Repositories;

public interface IWorkoutExerciseRepository
{
    public Task<IEnumerable<WorkoutExercise>> GetAllAsync(CancellationToken cancellationToken);
    public Task<WorkoutExercise> GetAsync(Guid workoutExerciseId, CancellationToken cancellationToken);
    public Task<Guid> CreateAsync(WorkoutExercise workout, CancellationToken cancellationToken);
    public Task UpdateOrderIndexAsync(Guid workoutExerciseMovedId, Guid workoutExerciseOverId, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid workoutId, CancellationToken cancellationToken);
}