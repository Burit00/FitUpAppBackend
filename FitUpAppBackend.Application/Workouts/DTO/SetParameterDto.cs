using FitUpAppBackend.Core.SetParameterNames.Enums;
using FitUpAppBackend.Core.SetParameters.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class SetParameterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Unit { get; set; }
    
    public SetParameterDto(SetParameter parameter)
    {
        Id = parameter.Id;
        Name = parameter.SetParameterName.Name.ToString();
        Value = parameter.Value;
        
        switch (parameter.SetParameterName.Name)
        {
            case SetParameterNameEnum.Weight:
                Unit = "kg";
                break;
            case SetParameterNameEnum.Distance:
                Unit = "m";
                break;
            case SetParameterNameEnum.Reps:
                Unit = "";
                break;
            case SetParameterNameEnum.Time:
                Unit = "s";
                break;
            default:
                Unit = "";
                break;
        }

    }
}