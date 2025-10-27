using FluentValidation;
using Skolar.Application.Todos.Commands;
using Skolar.Domain.Enums;

internal class CreateTodoCommandValidators : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidators()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Priority)
     .NotEmpty().WithMessage("Priority is required.")
     .IsEnumName(typeof(TodoPriority), caseSensitive: false)
     .WithMessage("Priority must be one of: Low, Medium, Normal, High, or Urgent.");



        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Due date must be in the future.");
    }
}
