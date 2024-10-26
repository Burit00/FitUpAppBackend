using FitUpAppBackend.Application.WorkoutExercises.DTO;
using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class WorkoutDto
{
    public Guid Id { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; }
    public DateTime Date { get; set; }
    
    public WorkoutDto(Workout workout)
    {
        Id = workout.Id;
        Date = workout.Date;
        Exercises = workout.WorkoutExercises
            .Select(we => new WorkoutExerciseDto(we))
            .ToList();
        
    }
}
