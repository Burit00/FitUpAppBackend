using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;

namespace FitUpAppBackend.Core.SetParameterNames.Entities;

public class SetParameterName : Entity
{
    public string Name { get; private set; }
    public IReadOnlyCollection<Exercise> Exercises => _exercises;
    public IReadOnlyCollection<SetParameter> SetParameters => _setParameters;

    private List<Exercise> _exercises = new();
    private List<SetParameter> _setParameters = new();

    private SetParameterName(string name)
    {
        Name = name;
    }

    public static SetParameterName Create(string name)
        => new(name);

    public void Update(string name)
    {
        Name = name;
    }
}