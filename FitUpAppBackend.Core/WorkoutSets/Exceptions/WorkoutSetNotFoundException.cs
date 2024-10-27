using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.WorkoutSets.Exceptions;

public sealed class WorkoutSetNotFoundException() : NotFoundFitUpException("Workout set not found with given id.")
{
    
}