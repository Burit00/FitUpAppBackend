using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Workouts.Exceptions;

public class WorkoutNotFoundException() : NotFoundFitUpException("Workout not found.");