using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public record BrowseWorkoutsDto(Guid WorkoutId, DateTimeOffset Date);

public static class WorkoutExtension
{
    public static BrowseWorkoutsDto AsDto(this Workout workout)
    {
        return new BrowseWorkoutsDto(workout.Id, workout.Date);
    }
}