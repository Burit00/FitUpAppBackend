using FitUpAppBackend.Core.Exercises.Exceptions;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.Exercises.Entities;

public sealed class Exercise : Entity
{
    public string Name { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public ExerciseCategory Category { get; private set; }

    private List<SetParameterName> _setParameters = new();
    public IReadOnlyCollection<SetParameterName> SetParameters => _setParameters;
    
    private Exercise(string name, ExerciseCategory category)
    {
        Name = name;
        Category = category;
        CategoryId = category.Id;
    }
    
    public static Exercise Create(string name, ExerciseCategory category)
        => new(name, category);

    public void Update(string name, ExerciseCategory category)
    {
        ValidateExerciseName(name);
        Name = name;
        CategoryId = category.Id;
        Category = category;
    }

    private static void ValidateExerciseName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyExerciseNameException();
        
        var MAX_EXERCISE_NAME_LENGTH = 50;
        if (name.Length > MAX_EXERCISE_NAME_LENGTH)
            throw new ExerciseNameIsTooLongException(MAX_EXERCISE_NAME_LENGTH);
        
    }
}