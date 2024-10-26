using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.CreateWorkoutExercise;

public class CreateWorkoutExerciseHandler : ICommandHandler<CreateWorkoutExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

    public CreateWorkoutExerciseHandler(IWorkoutExerciseRepository workoutExerciseRepository)
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }
    public async Task<CreateOrUpdateResponse> HandleAsync(CreateWorkoutExerciseCommand command, CancellationToken cancellationToken)
    {
        var workoutExercise = WorkoutExercise.Create(command.OrderIndex, command.ExerciseId, command.WorkoutId);
        var result = await _workoutExerciseRepository.CreateAsync(workoutExercise, cancellationToken);
        return new CreateOrUpdateResponse(result);
    }
}