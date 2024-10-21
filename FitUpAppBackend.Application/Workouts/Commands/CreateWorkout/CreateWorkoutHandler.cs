using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand, Guid>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateWorkoutHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }
    public async Task<Guid> HandleAsync(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        
        var workout = Workout.Create(_currentUserService.UserId, command.Date);
        var workoutId = await _workoutRepository.CreateAsync(workout, cancellationToken);
        return workoutId;
    }
}