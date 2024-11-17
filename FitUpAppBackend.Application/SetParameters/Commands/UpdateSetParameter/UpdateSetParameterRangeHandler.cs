using FitUpAppBackend.Core.SetParameters.Repositories;
using FitUpAppBackend.Core.WorkoutSets.Exceptions;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.SetParameters.Commands.UpdateSetParameter;

public class UpdateSetParameterRangeHandler: ICommandHandler<UpdateSetParameterRangeCommand>
{
    private readonly ISetParameterRepository _setParameterRepository;

    public UpdateSetParameterRangeHandler(ISetParameterRepository setParameterRepository)
    {
        _setParameterRepository = setParameterRepository;
    }
    
    public async Task HandleAsync(UpdateSetParameterRangeCommand command, CancellationToken cancellationToken)
    {
        var setParameters =
            (await _setParameterRepository.GetByWorkoutSetIdAsync(command.WorkoutSetId, cancellationToken)).ToList();
        
        foreach (var updatedParameter in command.Parameters)
        {
            var originalSetParameter = setParameters.FirstOrDefault(p => p.SetParameterNameId == updatedParameter.Id);
            if(originalSetParameter == null)
                throw new WorkoutSetHasInvalidParameterException();
            originalSetParameter.UpdateValue(updatedParameter.Value);
        }
        
        await _setParameterRepository.UpdateRangeAsync(setParameters, cancellationToken);
    }
}