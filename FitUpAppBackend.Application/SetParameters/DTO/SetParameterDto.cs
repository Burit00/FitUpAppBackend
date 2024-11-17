using FitUpAppBackend.Core.SetParameters.Entities;

namespace FitUpAppBackend.Application.SetParameters.DTO;

public sealed class SetParameterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    
    public SetParameterDto(SetParameter parameter)
    {
        Id = parameter.SetParameterNameId;
        Name = parameter.SetParameterName.Name;
        Value = parameter.Value;
        
    }
}