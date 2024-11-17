using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.WorkoutSets.Exceptions;
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

        if (workoutSet is null)
            throw new WorkoutSetNotFoundException();
        
        workoutSet.Update(command.OrderIndex);
        
        var result = await _workoutSetRepository.UpdateAsync(workoutSet, cancellationToken);
        
        return new CreateOrUpdateResponse(result);
    }
}