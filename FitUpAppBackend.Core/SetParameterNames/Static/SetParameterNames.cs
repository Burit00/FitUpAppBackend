namespace FitUpAppBackend.Core.SetParameterNames.Static;

public static class SetParameterNames
{
    public static readonly string Weight = nameof(Weight).ToLower();
    public static readonly string Reps = nameof(Reps).ToLower();
    public static readonly string Time = nameof(Time).ToLower();
    public static readonly string Distance = nameof(Distance).ToLower();
    
    
    private static List<string> _parameters;
    public static IReadOnlyList<string> Parameters => _parameters; 
    
    static SetParameterNames()
    {
        _parameters = new List<string>()
        {
            Weight,
            Reps,
            Time,
            Distance,
        };
    }
}