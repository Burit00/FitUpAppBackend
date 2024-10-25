using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.ExerciseCategories.Commands.CreateExerciseCategory;

public record CreateExerciseCategoryCommand(string Name) : ICommand<CreateOrUpdateResponse>;