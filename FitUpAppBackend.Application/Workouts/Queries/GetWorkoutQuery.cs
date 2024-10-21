using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.Queries;

public record GetWorkoutQuery(Guid WorkoutId) : IQuery<WorkoutDto>
{
    
}