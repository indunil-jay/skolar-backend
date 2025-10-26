using MediatR;
using Skolar.Application.Todos.Responses;
using Skolar.Domain;
using Skolar.Domain.Enums;
using Skolar.Domain.ValueObjects;

namespace Skolar.Application.Todos.Commands;

internal class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoResponse>

{

    public Task<TodoResponse> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
       var todo = Todo.Create(
            new TodoTitle(command.Title),
            new TodoDescription(command.Description),
           (TodoPriority)Enum.Parse(typeof(TodoPriority), command.Priority, true),
        command.DueDate);

        var response = new TodoResponse(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.Metadata.Priority,
            todo.Metadata.IsCompleted,
            todo.CreatedAt,
            todo.UpdatedAt,
            todo.Metadata.DueDate,
            todo.CompletedAt
        );

        return Task.FromResult(response); 
    }
}
