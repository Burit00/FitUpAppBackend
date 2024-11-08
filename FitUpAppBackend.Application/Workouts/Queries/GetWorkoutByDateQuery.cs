using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.Queries;

public sealed record GetWorkoutByDateQuery(DateTime Date) : IQuery<WorkoutDto>
{
    
}