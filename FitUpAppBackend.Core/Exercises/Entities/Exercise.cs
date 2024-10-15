using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Exercises.Exceptions;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;

namespace FitUpAppBackend.Core.Exercises.Entities;

public sealed class Exercise : Entity
{
    public string Name { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public ExerciseCategory Category { get; private set; }

    private List<SetParameterName> _setParameters = new();
    public IReadOnlyCollection<SetParameterName> SetParameters => _setParameters;
    
    private Exercise(string name, Guid categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
    
    public static Exercise Create(string name, Guid categoryId)
        => new(name, categoryId);

    public void UpdateName(string name)
    {
        ValidateExerciseName(name);
        Name = name;
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