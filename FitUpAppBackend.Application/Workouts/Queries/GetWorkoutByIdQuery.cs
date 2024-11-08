using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.Queries;

public record GetWorkoutByIdQuery(Guid WorkoutId) : IQuery<WorkoutDto>
{
    
}