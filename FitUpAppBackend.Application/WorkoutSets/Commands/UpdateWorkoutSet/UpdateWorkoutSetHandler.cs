using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutSets.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.UpdateWorkoutSet;

public class UpdateWorkoutSetHandler : ICommandHandler<UpdateWorkoutSetCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutSetRepository _workoutSetRepository;

    public UpdateWorkoutSetHandler(IWorkoutSetRepository workoutSetRepository)
    {
        _workoutSetRepository = workoutSetRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(UpdateWorkoutSetCommand command, CancellationToken cancellationToken)
    {
        var workoutSet = await _workoutSetRepository.GetByIdAsync(command.Id, cancellationToken);
        
        workoutSet.Update(command.OrderIndex);
        workoutSet.UpdateSetParameterRange(command.ParameterValues.Select(pv => SetParameter.Create(pv.Id, pv.Value)));
        
        var result = await _workoutSetRepository.UpdateAsync(workoutSet, cancellationToken);
        
        return new CreateOrUpdateResponse(result);
    }
}