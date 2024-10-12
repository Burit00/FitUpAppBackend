using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.SetParameterNames.Enums;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Shared.Abstractions.Entities;

namespace FitUpAppBackend.Core.SetParameterNames.Entities;

public class SetParameterName : Entity
{
    public SetParameterNameEnum Name { get; private set; }
    public IReadOnlyCollection<Exercise> Exercises => _exercises;
    public IReadOnlyCollection<SetParameter> SetParameters => _setParameters;

    private List<Exercise> _exercises = new();
    private List<SetParameter> _setParameters = new();

    private SetParameterName(SetParameterNameEnum name)
    {
        Name = name;
    }

    public static SetParameterName Create(SetParameterNameEnum name)
        => new(name);

    public void Update(SetParameterNameEnum name)
    {
        Name = name;
    }
}