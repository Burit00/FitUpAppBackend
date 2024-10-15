using System.Diagnostics.Contracts;
using FitUpAppBackend.Core.Abstractions.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.SetParameterNames.Enums;
using FitUpAppBackend.Core.SetParameters.Exceptions;
using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Core.SetParameters.Entities;

public sealed class SetParameter : Entity
{
    public SetParameterName Name { get; private set; }
    public string Value { get; private set; }
    public WorkoutSet WorkoutSet { get; private set; }

    private SetParameter(string value)
    {
        Value = value;
    }

    public static SetParameter Create(string value)
        => new SetParameter(value);

    public void UpdateValue(string value)
    {
        ValidateParameterValue(value, Name);
        Value = value;
    }

    private void ValidateParameterValue(string value, SetParameterName name)
    {
        switch (name.Name)
        {
            case SetParameterNameEnum.Weight:
                ValidateParameterWeightValue(value);
                break;
        }
    }

    private void ValidateParameterWeightValue(string value)
    {
        try
        {
            var valueInt = Convert.ToInt32(value);
            if (valueInt < 0)
                throw new WrongWeightValueException();
        }
        catch (FormatException e)
        {
            throw new WrongWeightValueException();
        }
    }
}