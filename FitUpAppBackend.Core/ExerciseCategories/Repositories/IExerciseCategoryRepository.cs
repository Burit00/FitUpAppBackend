using FitUpAppBackend.Core.ExerciseCategories.Entities;

namespace FitUpAppBackend.Core.ExerciseCategories.Repositories;

public interface IExerciseCategoryRepository
{
    Task<Guid> CreateAsync(ExerciseCategory exerciseCategory, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(ExerciseCategory exerciseCategory, CancellationToken cancellationToken);
    Task<ExerciseCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ExerciseCategory>> GetAllAsync(string search, CancellationToken cancellationToken);
}