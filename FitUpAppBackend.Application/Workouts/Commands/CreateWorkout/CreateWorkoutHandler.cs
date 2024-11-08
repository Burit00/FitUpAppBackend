using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateWorkoutHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }
    public async Task<CreateOrUpdateResponse> HandleAsync(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        var workout = Workout.Create(_currentUserService.UserId, command.Date);

        var orderIndex = 0;
        foreach (var exerciseId in command.ExerciseIds)
        {
            workout.AddExercise(WorkoutExercise.Create(orderIndex, exerciseId, workout.Id));
            ++orderIndex;
        }
        
        var workoutId = await _workoutRepository.CreateAsync(workout, cancellationToken);
        return new CreateOrUpdateResponse(workoutId);
    }
}