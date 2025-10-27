using MediatR;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Enums;
using Skolar.Domain.ValueObjects;

namespace Skolar.Application.Todos.Commands;
public sealed record CreateTodoCommand(TodoTitle Title, TodoDescription? Description, TodoPriority Priority, DateTime DueDate): IRequest<TodoResponse>;
