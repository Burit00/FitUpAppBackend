using FitUpAppBackend.Application.SetParameterNames.DTO;
using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Application.WorkoutExercises.DTO;

public sealed class WorkoutExerciseDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ExerciseId { get; set; }
    public string Category { get; set; }
    public Guid CategoryId { get; set; }
    public int OrderIndex { get; set; }
    public IEnumerable<WorkoutSetDto> Sets { get; set; }
    public IEnumerable<SetParameterNameDto> Parameters { get; set; }
    
    public WorkoutExerciseDetailsDto(WorkoutExercise workoutExercise)
    {
        Id = workoutExercise.Id;
        Name = workoutExercise.Exercise.Name;
        ExerciseId = workoutExercise.Exercise.Id;
        Category = workoutExercise.Exercise.Category.Name;
        CategoryId = workoutExercise.Exercise.Category.Id;
        OrderIndex = workoutExercise.OrderIndex;
        Sets = workoutExercise.WorkoutSets
            .OrderBy(ws => ws.OrderIndex)
            .Select(ws => new WorkoutSetDto(ws))
            .ToList();
        Parameters = workoutExercise.Exercise.SetParameters
            .OrderBy(p => p.Name)
            .Select(p => new SetParameterNameDto(p));
    }    
}