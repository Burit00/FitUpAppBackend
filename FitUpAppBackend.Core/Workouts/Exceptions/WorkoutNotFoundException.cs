using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Workouts.Exceptions;

public class WorkoutNotFoundException() : FitUpException("Not found workout with given Id.")
{
    
}