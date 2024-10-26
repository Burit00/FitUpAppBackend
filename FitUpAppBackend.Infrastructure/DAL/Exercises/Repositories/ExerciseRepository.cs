using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.Exercises.Exceptions;
using FitUpAppBackend.Core.Exercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Exercises.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Exercise> _exercises;

    public ExerciseRepository(EFContext context)
    {
        _context = context;
        _exercises = context.Exercises;
    }

    public async Task<Guid> CreateAsync(Exercise exercise, IEnumerable<Guid> setParameterNameIds, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        
        var doesExerciseAlreadyExists = await _exercises.AnyAsync(e
            => e.CategoryId.Equals(exercise.CategoryId) && e.Name.Equals(exercise.Name), cancellationToken);

        if (doesExerciseAlreadyExists)
            throw new ExerciseAlreadyExistsException(exercise.Name);

        var setParameterNames = await _context.SetParameterNames
            .Where(x => setParameterNameIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        exercise.AddSetParameterNameRange(setParameterNames);
        
        var result = await _exercises.AddAsync(exercise, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
        
        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        var result = _exercises.Update(exercise);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity.Id;
    }

    public async Task<Exercise> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _exercises.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

        if (result is null)
            throw new ExerciseNotFoundException(id);
        
        return result;
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync(string search, CancellationToken cancellationToken)
    {
        var result = _exercises
            .Where(e => e.Name.ToLower().Contains(search.ToLower()))
            .ToListAsync(cancellationToken);
        
        return await result;
    }
}