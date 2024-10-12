using System.Runtime.Serialization;

namespace FitUpAppBackend.Core.SetParameterNames.Enums;

public enum SetParameterNameEnum
{
    [EnumMember(Value = "weight")]
    Weight,
    [EnumMember(Value = "reps")]
    Reps,
    [EnumMember(Value = "time")]
    Time,
    [EnumMember(Value = "distance")]
    Distance,
    
}