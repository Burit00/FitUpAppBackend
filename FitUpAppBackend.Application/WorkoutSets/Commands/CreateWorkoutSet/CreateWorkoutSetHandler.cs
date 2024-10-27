using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;
using FitUpAppBackend.Core.WorkoutSets.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.CreateWorkoutSet;

public class CreateWorkoutSetHandler : ICommandHandler<CreateWorkoutSetCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutSetRepository _workoutSetRepository;

    public CreateWorkoutSetHandler(IWorkoutSetRepository workoutSetRepository)
    {
        _workoutSetRepository = workoutSetRepository;
    }
    
    public async Task<CreateOrUpdateResponse> HandleAsync(CreateWorkoutSetCommand command, CancellationToken cancellationToken)
    {
        var workoutSet = WorkoutSet.Create(command.OrderIndex, command.WorkoutExerciseId);

        
        var setParameters = command.ParameterValues
            .Select(pv => SetParameter.Create(pv.Id, pv.Value));
        
        workoutSet.AddSetParameterRange(setParameters);
        
        var result = await _workoutSetRepository.CreateAsync(workoutSet, cancellationToken);
        
        return new CreateOrUpdateResponse(result);
    }
}