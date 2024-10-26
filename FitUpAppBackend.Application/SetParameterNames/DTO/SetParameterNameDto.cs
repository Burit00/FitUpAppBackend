using FitUpAppBackend.Core.SetParameterNames.Entities;

namespace FitUpAppBackend.Application.SetParameterNames.DTO;

public sealed class SetParameterNameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public SetParameterNameDto(SetParameterName setParameterName)
    {
        Id = setParameterName.Id;
        Name = setParameterName.Name;
    }
}