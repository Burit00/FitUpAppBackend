using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.SetParameters.Exceptions;
using FitUpAppBackend.Core.SetParameters.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.SetParameters;

public class SetParameterRepository : ISetParameterRepository
{
    private readonly EFContext _context;
    private readonly DbSet<SetParameter> _setParameters;

    public SetParameterRepository(EFContext context)
    {
        _context = context;
        _setParameters = context.SetParameters;
    }
    
    public async Task<IEnumerable<SetParameter>> GetByWorkoutSetIdAsync(Guid workoutSetId, CancellationToken cancellationToken = default)
    {
        return await _setParameters.Where(p => p.WorkoutSetId == workoutSetId).ToListAsync(cancellationToken);
    }

    public async Task<SetParameter> GetByIdAsync(Guid setParameterId, CancellationToken cancellationToken = default)
    {
        var setParameter = await _setParameters.FirstOrDefaultAsync(p => p.Id == setParameterId, cancellationToken);

        if (setParameter == null)
            throw new SetParameterNotFoundException();
        
        return setParameter;
    }

    public async Task<Guid> UpdateAsync(SetParameter setParameter, CancellationToken cancellationToken = default)
    {
        var result = _setParameters.Update(setParameter);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity.Id;
    }

    public async Task UpdateRangeAsync(IEnumerable<SetParameter> setParameters, CancellationToken cancellationToken = default)
    {
        _setParameters.UpdateRange(setParameters);
        await _context.SaveChangesAsync(cancellationToken);
    }
}