using FitUpAppBackend.Core.ExerciseCategories.Entities;

namespace FitUpAppBackend.Application.ExerciseCategories.DTO;

public class ExerciseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ExerciseCategoryDto(ExerciseCategory exerciseCategory)
    {
        Id = exerciseCategory.Id;
        Name = exerciseCategory.Name;
    }
}