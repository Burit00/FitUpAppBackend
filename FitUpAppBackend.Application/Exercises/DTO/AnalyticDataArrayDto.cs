namespace FitUpAppBackend.Application.Exercises.DTO;

public class AnalyticDataArrayDto
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string ParameterName {get; set;}
    public IEnumerable<AnalyticDataValueDto> Values {get; set;}
}