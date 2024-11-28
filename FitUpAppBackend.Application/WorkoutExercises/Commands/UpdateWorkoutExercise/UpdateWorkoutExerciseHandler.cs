using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.UpdateWorkoutExercise;

public class UpdateWorkoutExerciseHandler : ICommandHandler<UpdateWorkoutExerciseCommand>
{
    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

    public UpdateWorkoutExerciseHandler(IWorkoutExerciseRepository workoutExerciseRepository)
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }

    public async Task HandleAsync(UpdateWorkoutExerciseCommand command, CancellationToken cancellationToken)
    {
        await _workoutExerciseRepository.UpdateOrderIndexAsync(command.WorkoutExerciseMovedId, command.WorkoutExerciseOverId, cancellationToken);
    }
}