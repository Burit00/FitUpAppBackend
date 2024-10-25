using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.ExerciseCategories.Commands.UpdateExerciseCategory;

public record UpdateExerciseCategoryCommand(Guid Id,string Name) : ICommand<CreateOrUpdateResponse>;