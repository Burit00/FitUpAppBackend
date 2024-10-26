using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;

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

    public void Update(string? name, Guid? categoryId)
    {
        Name = name ?? Name;
        CategoryId = categoryId ?? CategoryId;
    }
}