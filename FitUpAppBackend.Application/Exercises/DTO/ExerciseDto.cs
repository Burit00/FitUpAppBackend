using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Core.Exercises.Entities;

namespace FitUpAppBackend.Application.Exercises.DTO;

public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ExerciseCategoryDto Category { get; set; }
    
    public ExerciseDto(Exercise exercise)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = new ExerciseCategoryDto(exercise.Category);
    }
}