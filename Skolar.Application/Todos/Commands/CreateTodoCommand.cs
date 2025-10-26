using MediatR;
using Skolar.Application.Todos.Responses;

namespace Skolar.Application.Todos.Commands;
public sealed record CreateTodoCommand(string Title, string? Description, string Priority, DateTime DueDate): IRequest<TodoResponse>;
