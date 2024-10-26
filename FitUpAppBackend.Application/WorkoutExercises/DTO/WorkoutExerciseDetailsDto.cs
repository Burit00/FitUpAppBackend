using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Application.WorkoutExercises.DTO;

public sealed class WorkoutExerciseDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int OrderIndex { get; set; }
    public IEnumerable<WorkoutSetDto> Sets { get; set; }
    public IEnumerable<string> Parameters { get; set; }
    
    public WorkoutExerciseDetailsDto(WorkoutExercise workoutExercise)
    {
        Id = workoutExercise.Id;
        Name = workoutExercise.Exercise.Name;
        Category = workoutExercise.Exercise.Category.Name;
        OrderIndex = workoutExercise.OrderIndex;
        Sets = workoutExercise.WorkoutSets
            .Select(ws => new WorkoutSetDto(ws))
            .ToList();
        Parameters = workoutExercise.Exercise.SetParameters
            .Select(p => p.Name.ToString());
    }    
}