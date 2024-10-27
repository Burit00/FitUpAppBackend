using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Core.SetParameters.Entities;

public sealed class SetParameter : Entity
{
    public Guid SetParameterNameId { get; private set; }
    public SetParameterName SetParameterName { get; private set; }
    public string Value { get; private set; }
    public Guid WorkoutSetId { get; private set; }
    public WorkoutSet WorkoutSet { get; private set; }

    private SetParameter(Guid setParameterNameId, string value)
    {
        SetParameterNameId = setParameterNameId;
        Value = value;
    }

    public static SetParameter Create(Guid setParameterNameId, string value)
        => new SetParameter(setParameterNameId, value);

    public void UpdateValue(string value)
    {
        Value = value;
    }

}