using FitUpAppBackend.Core.WorkoutSets.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutSets.Commands.DeleteWorkoutSet;

public class DeleteWorkoutSetHandler : ICommandHandler<DeleteWorkoutSetCommand>
{
    private readonly IWorkoutSetRepository _workoutSetRepository;

    public DeleteWorkoutSetHandler(IWorkoutSetRepository workoutSetRepository)
    {
        _workoutSetRepository = workoutSetRepository;
    }

    public async Task HandleAsync(DeleteWorkoutSetCommand command, CancellationToken cancellationToken)
    {
        await _workoutSetRepository.DeleteAsync(command.Id, cancellationToken);
    }
}