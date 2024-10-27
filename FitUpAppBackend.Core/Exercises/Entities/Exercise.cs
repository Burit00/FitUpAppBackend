using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;

namespace FitUpAppBackend.Core.Exercises.Entities;

public sealed class Exercise : Entity
{
    public string Name { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public ExerciseCategory Category { get; private set; }

    private List<SetParameterName> _setParameters = new();
    public IReadOnlyCollection<SetParameterName> SetParameters => _setParameters;
    
    private List<WorkoutExercise> _workoutExercises = new();
    public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises;
    
    private Exercise(string name, Guid categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
    
    public static Exercise Create(string name, Guid categoryId)
        => new(name, categoryId);

    public void Update(string? name, Guid? categoryId)
    {
        Name = name ?? Name;
        CategoryId = categoryId ?? CategoryId;
    }

    public void AddSetParameterNameRange(IEnumerable<SetParameterName> setParameterNames)
    {
        _setParameters.AddRange(setParameterNames);
    }
}