using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class WorkoutDto
{
    public Guid Id { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; }
    public DateTimeOffset Date => _date.Date;

    private DateTimeOffset _date;
    public WorkoutDto(Workout workout)
    {
        Id = workout.Id;
        _date = workout.Date;
        Exercises = workout.WorkoutExercises
            .Select(we => new WorkoutExerciseDto(we))
            .ToList();
        
    }
}
