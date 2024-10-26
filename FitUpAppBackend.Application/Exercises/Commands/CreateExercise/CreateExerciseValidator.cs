using FluentValidation;

namespace FitUpAppBackend.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(3).WithMessage("Name cannot be less than 3 characters")
            .MaximumLength(50).WithMessage("Name cannot be more than 50 characters");
    }
}