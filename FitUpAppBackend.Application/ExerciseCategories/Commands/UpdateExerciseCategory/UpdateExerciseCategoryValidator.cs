using FitUpAppBackend.Application.ExerciseCategories.Commands.CreateExerciseCategory;
using FluentValidation;

namespace FitUpAppBackend.Application.ExerciseCategories.Commands.UpdateExerciseCategory;

public class UpdateExerciseCategoryValidator : AbstractValidator<CreateExerciseCategoryCommand>
{
    public UpdateExerciseCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(3).WithMessage("Name cannot be less than 3 characters")
            .MaximumLength(50).WithMessage("Name cannot be more than 50 characters");
    }
}