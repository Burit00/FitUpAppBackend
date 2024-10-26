using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.ExerciseCategories.Exceptions;
using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;

public class ExerciseCategoryRepository : IExerciseCategoryRepository
{
    private readonly EFContext _context;
    private readonly DbSet<ExerciseCategory> _exerciseCategories;

    public ExerciseCategoryRepository(EFContext context)
    {
        _context = context;
        _exerciseCategories = _context.ExerciseCategories;
    }
    
    public async Task<Guid> CreateAsync(ExerciseCategory exerciseCategory, CancellationToken cancellationToken)
    {
        var doesExerciseCategoryExist = await _exerciseCategories.AnyAsync(ec => ec.Name.Equals(exerciseCategory.Name), cancellationToken);

        if (doesExerciseCategoryExist)
            throw new ExerciseCategoryAlreadyExistsException(exerciseCategory.Name);
        
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        var result = await _exerciseCategories.AddAsync(exerciseCategory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(ExerciseCategory exerciseCategory, CancellationToken cancellationToken)
    {
        var result = _exerciseCategories.Update(exerciseCategory);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity.Id;
    }

    public async Task<ExerciseCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
       var exerciseCategory = await _exerciseCategories.FirstOrDefaultAsync(ec => ec.Id.Equals(id), cancellationToken);

       if (exerciseCategory == null)
           throw new ExerciseCategoryNotFoundException(id);
       
       return exerciseCategory;
    }

    public async Task<IEnumerable<ExerciseCategory>> GetAllAsync(string search, CancellationToken cancellationToken)
    {
        var exerciseCategories = await _exerciseCategories
            .Where(ec => ec.Name.ToLower().Contains(search.ToLower()))
            .ToListAsync(cancellationToken);
        
        return exerciseCategories;
    }
}