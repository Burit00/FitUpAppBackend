using FitUpAppBackend.Core.SetParameters.Entities;

namespace FitUpAppBackend.Core.SetParameters.Repositories;

public interface ISetParameterRepository
{
    Task<IEnumerable<SetParameter>> GetByWorkoutSetIdAsync(Guid workoutSetId, CancellationToken cancellationToken);
    Task<SetParameter> GetByIdAsync(Guid setParameterId, CancellationToken cancellationToken);
    
    Task<Guid> UpdateAsync(SetParameter setParameter, CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<SetParameter> setParameters, CancellationToken cancellationToken);
}