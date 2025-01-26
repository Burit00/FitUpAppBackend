using FitUpAppBackend.Application.SetParameterNames.DTO;
using FitUpAppBackend.Core.Exercises.Entities;

namespace FitUpAppBackend.Application.Exercises.DTO;

public sealed class ExerciseDetailsDto : ExerciseDto
{
    public IEnumerable<SetParameterNameDto> SetParameterNames { get; set; }

    public ExerciseDetailsDto(Exercise exercise) : base(exercise)
    {
        SetParameterNames = exercise.SetParameters
            .Select(p => new SetParameterNameDto(p)).ToList();
    }
}