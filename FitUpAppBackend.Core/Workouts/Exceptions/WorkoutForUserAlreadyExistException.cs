using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutForUserAlreadyExistException() 
    : BadRequestFitUpException("Workout for user already exists.");