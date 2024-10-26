using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class WorkoutExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int OrderIndex { get; set; }
    public List<WorkoutSetDto> Sets { get; set; }
    
    public WorkoutExerciseDto(WorkoutExercise workoutExercise)
    {
        Id = workoutExercise.Id;
        Name = workoutExercise.Exercise.Name;
        Category = workoutExercise.Exercise.Category.Name;
        OrderIndex = workoutExercise.OrderIndex;
        Sets = workoutExercise.WorkoutSets
            .Select(ws => new WorkoutSetDto(ws))
            .ToList();
    }    
}