using Skolar.Application.Primitives;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos.Enums;
using Skolar.Domain.Todos.ValueObjects;

namespace Skolar.Application.Todos.Commands;

public sealed record CreateTodoCommand(string Title, string? Description, TodoPriority Priority, DateTime? DueDate) : ICommand<TodoResponse>;
