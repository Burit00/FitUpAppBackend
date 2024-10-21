using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Application.Workouts.DTO;

public sealed class WorkoutSetDto
{
    public Guid Id { get; set; }
    public uint OrderIndex { get; set; }
    public List<SetParameterDto> Parameters { get; set; }
    
    public WorkoutSetDto(WorkoutSet workoutSet)
    {
        Id = workoutSet.Id;
        OrderIndex = workoutSet.OrderIndex;
        Parameters = workoutSet.SetParameters
            .Select(sp => new SetParameterDto(sp))
            .ToList();
    }    
}