using FluentValidation;
using Skolar.Domain.Todos.Enums;

namespace Skolar.Application.Todos.Commands
{
    public sealed class CreateTodoCommandValidators : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidators()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(Messages.TitleRequired)
                .MinimumLength(3).WithMessage(Messages.TitleTooShort)
                .MaximumLength(64).WithMessage(Messages.TitleTooLong);

            RuleFor(x => x.Description)
                .Must(desc => desc is null || (desc.Length <= 256))
                .WithMessage(Messages.DescriptionTooLong);

            RuleFor(x => x.Priority.ToString())
                .NotEmpty().WithMessage(Messages.PriorityRequired)
                .Must(value => Enum.TryParse<TodoPriority>(value, true, out _))
                .WithMessage(Messages.PriorityInvalid);

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now)
                .WithMessage(Messages.DueDateInPast);
        }


        private static class Messages
        {
            public const string TitleRequired = "Title is required.";
            public const string TitleTooShort = "Title must have at least 3 characters.";
            public const string TitleTooLong = "Title must not exceed 64 characters.";

            public const string DescriptionTooLong = "Description must not exceed 256 characters.";

            public const string PriorityRequired = "Priority is required.";
            public const string PriorityInvalid = "Priority must be one of: Low, Medium, Normal, High, or Urgent.";

            public const string DueDateInPast = "Due date must be in the future.";
        }
    }
}