using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Exercises.Entities;

namespace FitUpAppBackend.Core.ExerciseCategories.Entities;

public sealed class ExerciseCategory : Entity
{
    
    private List<Exercise> _exercises;
    public string Name { get; private set; }
    public IReadOnlyCollection<Exercise> Exercises => _exercises;

    private ExerciseCategory(string name)
    {
        Name = name;
    }

    public static ExerciseCategory Create(string name)
        => new(name);

    public void Update(string name)
    {
        Name = name;
    }
}