using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.ExerciseCategories.Exceptions;

public sealed class ExerciseCategoryNotFoundException(Guid id) : NotFoundFitUpException($"Not found exercise category with given id: {id}.");