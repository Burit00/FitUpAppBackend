using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.WorkoutExercises.Exceptions;

public class WorkoutExerciseNotFoundException() : NotFoundFitUpException("Not found workout exercise with given Id.");
