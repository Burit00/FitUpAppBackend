using FitUpAppBackend.Core.Exercises.Entities;

namespace FitUpAppBackend.Core.Exercises.Repositories;

public interface IExerciseRepository
{
    Task<Guid> CreateAsync(Exercise exerciseCategory, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Exercise exerciseCategory, CancellationToken cancellationToken);
    Task<Exercise> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Exercise>> GetAllAsync(string search, CancellationToken cancellationToken);
    
}