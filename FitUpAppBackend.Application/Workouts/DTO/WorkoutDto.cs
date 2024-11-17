using FitUpAppBackend.Application.WorkoutExercises.DTO;
using FitUpAppBackend.Core.Workouts.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class WorkoutDto
{
    public Guid Id { get; set; }
    public List<WorkoutExerciseDetailsDto> Exercises { get; set; }
    public DateTime Date { get; set; }
    
    public WorkoutDto(Workout workout)
    {
        Id = workout.Id;
        Date = workout.Date;
        Exercises = workout.WorkoutExercises
            .OrderBy(we => we.OrderIndex)
            .Select(we => new WorkoutExerciseDetailsDto(we))
            .ToList();
        
    }
}
