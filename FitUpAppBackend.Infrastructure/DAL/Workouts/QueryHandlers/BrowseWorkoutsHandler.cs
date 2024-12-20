using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Application.Workouts.Queries;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Workouts.QueryHandlers;

public sealed class BrowseWorkoutsHandler : IQueryHandler<BrowseWorkoutsQuery, IEnumerable<BrowseWorkoutsDto>>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public BrowseWorkoutsHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<BrowseWorkoutsDto>> HandleAsync(BrowseWorkoutsQuery query,
        CancellationToken cancellationToken)
    {
        var workouts = _context.Workouts.Where(w => w.UserId == _currentUserService.UserId);

        var isCategoriesEmpty = query.Categories is null || !query.Categories.Any();
        if (!isCategoriesEmpty)
        {
            workouts = workouts
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
                .ThenInclude(e => e.Category)
                .Where(w => query.Categories.All(c => w.WorkoutExercises.Any(we => we.Exercise.CategoryId.Equals(c))));
        }

        if (query.DateStart is not null)
            workouts = workouts.Where(w => w.Date >= query.DateStart.Value);
        
        if (query.DateEnd is not null)
            workouts = workouts.Where(w => w.Date <= query.DateEnd.Value);
        
        var workoutList = await workouts.ToListAsync(cancellationToken);
        return workoutList.Select(w => new BrowseWorkoutsDto(w.Id, w.Date));
    }
}