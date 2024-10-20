using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.BrowseWorkouts;

public class BrowseWorkoutsHandler : IQueryHandler<BrowseWorkoutsQuery, IEnumerable<BrowseWorkoutsDto>>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public BrowseWorkoutsHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }
    public async Task<IEnumerable<BrowseWorkoutsDto>> HandleAsync(BrowseWorkoutsQuery query, CancellationToken cancellationToken)
    {
        var workouts = await _workoutRepository.GetAllForUserAsync(_currentUserService.UserId, cancellationToken);
        
        return workouts.Select(w => w.AsDto());
    }
}