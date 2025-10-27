using FluentValidation;
using Skolar.Application.Todos.Commands;
using Skolar.Domain.Enums;

internal class CreateTodoCommandValidators : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidators()
    {
        RuleFor(x => x.Title.Value)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(64).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
        .Must(desc => desc == null || (desc.Value != null && desc.Value.Length <= 256))
        .WithMessage("Description must not exceed 100 characters.");

        RuleFor(x => x.Priority.ToString())
            .NotEmpty().WithMessage("Priority is required.")
            .Must(value => !string.IsNullOrEmpty(value) && Enum.TryParse<TodoPriority>(value, true, out _))
            .WithMessage("Priority must be one of: Low, Medium, Normal, High, or Urgent.");



        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Due date must be in the future.");
    }
}



