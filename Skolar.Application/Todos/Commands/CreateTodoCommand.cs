using Skolar.Application.Primitives;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos.Enums;
using Skolar.Domain.Todos.ValueObjects;

namespace Skolar.Application.Todos.Commands;

public sealed record CreateTodoCommand(TodoTitle Title, TodoDescription? Description, TodoPriority Priority, DateTime? DueDate) : ICommand<TodoResponse>;
