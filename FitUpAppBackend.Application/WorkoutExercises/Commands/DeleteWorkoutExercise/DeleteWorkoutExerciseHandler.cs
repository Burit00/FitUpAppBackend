using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.DeleteWorkoutExercise;

public class DeleteWorkoutExerciseHandler : ICommandHandler<DeleteWorkoutExerciseCommand>
{
    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

    public DeleteWorkoutExerciseHandler(IWorkoutExerciseRepository workoutExerciseRepository)
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }
    
    public async Task HandleAsync(DeleteWorkoutExerciseCommand command, CancellationToken cancellationToken)
    {
        await _workoutExerciseRepository.DeleteAsync(command.WorkoutExerciseId, cancellationToken);
    }
}