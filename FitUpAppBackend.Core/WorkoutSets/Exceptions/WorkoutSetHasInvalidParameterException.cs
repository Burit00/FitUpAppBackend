using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.WorkoutSets.Exceptions;

public sealed class WorkoutSetHasInvalidParameterException() : BadRequestFitUpException("Workout set has an invalid parameter.")
{
    
}